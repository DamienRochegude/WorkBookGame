using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Variables priv�es accessibles depuis l'�diteur
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canDoubleJump = true;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance = 5.0f;
    [SerializeField] private LayerMask layermask;

    public float MoveSpeed { set { moveSpeed = value; } }
    public float JumpVelocity { set { jumpVelocity = value; } }
    public float DashSpeed { set { dashSpeed = value; } }
    public float DashDistance { set { dashDistance = value; } }
    public Rigidbody2D RB { get { return rb ; } }


    // Variables priv�es
    private float horizontalInput;
    private float verticalInput;
    private float dashTime;
    private Vector2 DashDirection;
    private bool usingJoystick;
    private Vector2 posJoueur;
    private Vector2 dest;
    private Animator anim, playerAnim;
    // Fonctions
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CheckJoystick();
        StartCoroutine(delayCheck());
        foreach (Transform t in transform)
        {
            if (t.name == "Leg")
            {
                playerAnim = t.gameObject.GetComponent<Animator>();
            }
        }
    }

    IEnumerator delayCheck()
    {
        CheckJoystick();
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(delayCheck());
    }

    private void Update()
    {
        //On va changer la valeur Speed de l'animator controller

        //On r�cup�re les inputs du joueurs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //On verifie si on utilise un clavier pour normaliser le dash
        Debug.DrawLine(rb.position, rb.position + dest);
        //Ici on v�rifie si le joueur peut dash
        if (canDash)
        {

            if (Input.GetButtonDown("Dash"))
            {
                //On les mets dans un Vector2
                DashDirection.Set(horizontalInput, verticalInput);
                if (!usingJoystick)
                {
                    DashDirection.Normalize();
                }
                dest = DashDirection * dashDistance;
                //On verifie si il y a une direction de dash, si ce n'est pas le cas il n'y a pas dash
                if (DashDirection.magnitude < 0.1)
                {
                    isDashing = false;
                }
                else
                {
                    //On recupere la position du joueur
                    posJoueur = rb.position;
                    playerAnim.SetFloat("yDash", DashDirection.y);
                    isDashing = true;
                    canDash = false;
                }
            }
        }
        //Ici est g�r� le saut et le double saut
        if (Input.GetButtonDown("Jump"))
        {
            //On v�rifie si le joueur peut sauter
            if (canJump)
            {
                // Le premier saut est d�tect� par GroundCheck
                SoundManager.PlaySound(7);
                rb.velocity = Vector2.up * jumpVelocity;
                canJump = false;
            }
            //On v�rifie si le joueur peut faire un double saut
            else if (canDoubleJump)
            {
                SoundManager.PlaySound(7);
                canDoubleJump = false; // Celui-ci d�pend de si on a d�j� saut�
                rb.velocity = Vector2.up * jumpVelocity;
            }
        }


    }
    private void FixedUpdate()
    {
        GroundCheck();
        //Cette partie s'execute quand le joueur dash
        if (isDashing)
        {
            SoundManager.PlaySound(3);
            if (rb.velocity.magnitude == 0 && verticalInput == 0)
            {
                isDashing = false;
            }
            else
            {             
                //On va acc�lerer son d�placement pendant un certain temps, qui est g�r� par le currentDashTimer
                rb.velocity = DashDirection * dashSpeed;
                canDash = false;
                if (Vector2.Distance(posJoueur, rb.position) > dashDistance)
                {
                    isDashing = false;
                    rb.velocity = Vector2.up * verticalInput;
                }
            }

        }
        //Ici on r�tablit la vitesse initial du joueur
        if (!isDashing)
        {
            float horizontalVelocity = horizontalInput * moveSpeed;
            rb.velocity = (Vector2.up * rb.velocity.y) + (Vector2.right * horizontalVelocity);
        }

    }

    //Cette fonction calcul l'espace entre le joueur et une surface possedant le tag "Ground" et passe les booleens � true si le joueur touche le sol
    private void GroundCheck()
    {

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.down, 4f, layermask);
        Collider2D colDash = Physics2D.OverlapBox(transform.position, new Vector2(.6f, .5f), 0f, layermask);
        if (0 < hit.distance && hit.distance < .1f) // 0 parfait = pas de collision
        {
            Debug.DrawRay(raycastOrigin.position, Vector2.down * hit.distance, Color.green);
            canJump = true; canDoubleJump = true; canDash = true;
        }
        else
        {
            Debug.DrawRay(raycastOrigin.position, Vector2.down * hit.distance, Color.red);
            canJump = false;
        }
        //On verifie si le joueur ne dash pas contre un mur
        if (colDash != null)
        {
            isDashing = false;
        }
    }
    private void CheckJoystick()
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            usingJoystick = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void ResetDash()
    {
        canDash = true;
    }

}