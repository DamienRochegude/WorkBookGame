using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Body;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask gmask;

    private PlayerController PlayerCScript;

    private Vector3 LastPos;

    private Animator anim;

    private float ySpd;


    void Start()
    {
        LastPos = transform.position;
        anim = GetComponent<Animator>();
        PlayerCScript = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LastPos.x < transform.position.x && transform.localScale.x == -1 || LastPos.x > transform.position.x && transform.localScale.x == 1)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }

        if (LastPos.x != transform.position.x)
        {
            anim.SetBool("IsMoving", true);
            //Debug.Log("isMoving = vrai");
        }
        else if (LastPos.x == transform.position.x)
        {
            anim.SetBool("IsMoving", false);
            //Debug.Log("isMoving = faux");
        }
        GroundCheck();
        anim.SetBool("isDashing",PlayerCScript.IsDashing());
        anim.SetFloat("Yspeed", transform.position.y - LastPos.y);
        LastPos = transform.position;
    }

    void ChangeAnimBody(int i)
    {
        Body.GetComponent<Animator>().SetInteger("State", i);
    }
    //0 isMoving
    //1 is Idle
    //2 is Jumping
    //3 is Dashing

    private void GroundCheck()
    {

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.down, 4f, gmask);
        if (0 < hit.distance && hit.distance < .1f) // 0 parfait = pas de collision
        {
            anim.SetBool("IsGrounded", true);
            //Debug.Log("IsGrounded = true");
        }
        else
        {
            anim.SetBool("IsGrounded", false);
            //Debug.Log("IsGrounded = nope");
        }
    }
}
