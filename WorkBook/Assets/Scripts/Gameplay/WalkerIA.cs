using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerIA : MonoBehaviour
{
    [SerializeField]
    private int Speed, TimeToTurn;

    [SerializeField]
    private LayerMask groundMask, noEnemyMask;

    private bool isGoingRight, wait = false;

    private Animator anim;

    private RaycastHit2D rc, rcWall;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        noEnemyMask += groundMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x == -1 && Speed > 0 || transform.localScale.x == 1 && Speed < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }

        rc = Physics2D.Raycast(transform.position + new Vector3(Speed*Time.deltaTime, 0, 0), Vector2.down, 1f, groundMask);
        rcWall = Physics2D.Raycast(transform.position + new Vector3(Speed * Time.deltaTime, 0, 0), Vector2.right , 0.1f, noEnemyMask);
        Debug.DrawRay(transform.position + new Vector3(Speed*Time.deltaTime, 0, 0), Vector2.down, Color.green);
        if (!rc && !wait || rcWall && !wait)
        {
            Speed = -Speed;
            wait = true;
            anim.SetBool("isWaiting", true);
            Invoke("unWait", TimeToTurn);
        }

        if (!wait)
        {
            transform.parent.Translate(new Vector3(Speed*Time.deltaTime, 0, 0));
        }
        else
        {
            transform.parent.Translate(new Vector3(0, 0, 0));
        }
    }

    void unWait()
    {
        wait = false;
        anim.SetBool("isWaiting", false);
    }
}
