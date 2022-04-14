using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosConvert : MonoBehaviour
{
    [Header("Mettre ce script dans un Game Object vide puis assignez ce transform au camera controller")]

    [Header("The transform of the player")]
    [SerializeField]
    [Tooltip("Bah le joueur quoi")]
    private Transform Player;
    [SerializeField]
    [Tooltip("Le transform du cahier")]
    private Transform Book;
    [SerializeField]
    [Tooltip("Le transform du cahier")]
    private Transform LeftPage, RightPage;

    [SerializeField]
    [Tooltip("Délai suivi")]
    private float SmoothTime = 3f;

    private Renderer RPageBoundaries, LPageBoundaries, PageBoundaries, BookBoundaries;

    private float minX, minY, maxX, maxY, bookMinX, bookMinY, bookMaxX, bookMaxY, xPos, yPos, zPos, velx, vely;

    private Rigidbody2D PlayerRb;

   


    // Start is called before the first frame update
    void Start()
    {
        CameraManager.PlayerPosConver = gameObject;
        //Left Page setup
        Renderer rd = null;
        foreach (Transform tr in LeftPage)      //Cherche le renderer de la page gauche
        {
            if (tr.gameObject.layer == 8)
            {
                foreach (Transform tr2 in tr)
                {
                    if (tr2.name == "Background")
                    {
                        rd = tr2.GetComponent<Renderer>();
                        LPageBoundaries = rd;
                    }
                }
            }
        }
        foreach (Transform tr in RightPage) //Cherche le renderer de la page droite
        {
            if (tr.gameObject.layer == 8)
            {
                foreach (Transform tr2 in tr)
                {
                    if (tr2.name == "Background")
                    {
                        rd = tr2.GetComponent<Renderer>();
                        RPageBoundaries = rd;
                    }
                }
            }
        }
        BookBoundaries = Book.GetComponent<Renderer>();

        ComputeBoundaries(LPageBoundaries, RPageBoundaries, BookBoundaries); //Calcule les minimum et maxi
    }

    // Update is called once per frame
    void Update() //Conversion de place du joueur dans le niveau dans le livre
    {
        if(Player == null)
        {
            Player = PlayerManager.GetPlayer().transform;
        }

        xPos = bookMinX + ((bookMaxX - bookMinX) * ((Player.position.x - minX) / (maxX - minX)));
        zPos = bookMinY + ((bookMaxY - bookMinY) * ((Player.position.y - minY) / (maxY - minY)));
        transform.position = new Vector3(xPos, yPos, zPos);

        //Debug.Log("Position = " + transform.position + " | BookMinX = " + bookMinX);
    }

    public void SetLeftPage(Transform t) //Cahnger la page gauche
    {
        LeftPage = t;
        setPageRenderer(t, false);
    }
    public void SetRightPage(Transform t) //Changer la page droite
    {
        RightPage = t;
        setPageRenderer(t, true);
    }

    private void setPageRenderer(Transform t, bool isRightPage) //Cherche les renderer des pages
    {
        Renderer rd = null;
        foreach (Transform tr in t)
        {
            if (tr.gameObject.layer == 8)
            {
                foreach (Transform tr2 in tr)
                {
                    if (tr2.name == "Background")
                    {
                        rd = tr2.GetComponent<Renderer>();
                    }
                }
            }
        }

        if (isRightPage)
        {
            RPageBoundaries = rd;

        }
        else
        {
            LPageBoundaries = rd;
        }
        ComputeBoundaries(LPageBoundaries, RPageBoundaries, BookBoundaries);
    }

    private void ComputeBoundaries(Renderer Lpage, Renderer Rpage, Renderer Book) //Calcule les minimum et maxi pour la conversion
    {
        minX = Lpage.bounds.center.x - (Lpage.bounds.size.x / 2);
        minY = Lpage.bounds.center.y - (Lpage.bounds.size.y / 2);

        maxX = Rpage.bounds.center.x + (Rpage.bounds.size.x / 2);
        maxY = Rpage.bounds.center.y + (Rpage.bounds.size.y / 2);

        bookMinX = Book.bounds.center.x - (Book.bounds.size.x / 2);
        bookMaxX = Book.bounds.center.x + (Book.bounds.size.x / 2);
        bookMinY = Book.bounds.center.z - (Book.bounds.size.z / 2);
        bookMaxY = Book.bounds.center.z + (Book.bounds.size.z / 2);

        yPos = Book.transform.position.y;
    }
}