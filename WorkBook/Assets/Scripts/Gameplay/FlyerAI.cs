using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAI : MonoBehaviour
{
    [SerializeField]
    private int Speed = 5, MaxDistanceFromSpawn = 50, DistanceToDetectPlayer = 25;
    [SerializeField]
    private LayerMask GroundLayer, PlayerLayer;

    private Rigidbody2D rb;
    private Animator anim;

    private GameObject player;

    private Vector3 PosGoal, StartPos, lastPos;

    private bool isLaunched = false, canMove = true, isChasing = false;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.GetPlayer();
        StartPos = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        newRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (isChasing && canMove)
            {
                PosGoal = player.transform.position;
                RaycastHit2D rc = Physics2D.Raycast(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), DistanceToDetectPlayer, GroundLayer); ;
                if (rc)
                {
                    //Debug.Log("NewRandomPos Launched");
                    rb.velocity = Vector2.zero;
                    Invoke("newRandomPos", 3f);
                    isLaunched = true;
                    isChasing = false;
                    rb.angularVelocity = 0;
                    rb.velocity = Vector2.zero;
                    anim.SetBool("isChasing", false);
                    canMove = false;
                    StartPos = transform.position;
                }
            }
            else if (!isChasing)
            {
                RaycastHit2D rc = Physics2D.Raycast(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2)), GroundLayer);
                if (!rc)
                {
                    canMove = true;
                    isChasing = true;
                    anim.SetBool("isChasing", true);
                    CancelInvoke();
                }
            }
        }
        else
        {
            player = PlayerManager.GetPlayer();
        }

        if ((PosGoal - transform.position).sqrMagnitude > Speed && canMove)
        {

            rb.velocity = -(transform.position - PosGoal).normalized * Speed;

            if (lastPos == transform.position && isLaunched)
            {
                PosGoal = transform.position;
            }
            lastPos = transform.position;

            isLaunched = false;
            //Debug.Log("Moving !! velocity = " + rb.velocity);

            if ((rb.velocity.x < 0 && transform.localScale.x > 0) || (rb.velocity.x > 0 && transform.localScale.x < 0))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (!isLaunched)
        {
            //Debug.Log("NewRandomPos Launched");
            rb.velocity = Vector2.zero;
            Invoke("newRandomPos", 3f);
            isLaunched = true;
            canMove = false;
        }
    }
    private bool IsNewPositionPossible()
    {
        RaycastHit2D rc = Physics2D.Raycast(transform.position, new Vector2(PosGoal.x - transform.position.x, PosGoal.y - transform.position.y), Mathf.Sqrt(Mathf.Pow(PosGoal.x - transform.position.x, 2) + Mathf.Pow(PosGoal.y - transform.position.y, 2)), GroundLayer);
        if (rc)
        {
            //if (rc.distance < 3)
            //{
            return false;
            //}
        }
        return true;
    }

    private void newRandomPos()
    {
        anim.SetBool("isChasing", false);
        int N = 0; 
        PosGoal = new Vector3(Random.Range(StartPos.x - MaxDistanceFromSpawn / 2, StartPos.x + MaxDistanceFromSpawn / 2), Random.Range(StartPos.y - MaxDistanceFromSpawn / 2, StartPos.y + MaxDistanceFromSpawn / 2), transform.position.z);
        while (!IsNewPositionPossible())
        {
            if (N == 100) { StartPos = transform.position; } //si le flyer ne trouve pas de nouvelle pos accessible après 100 essais il change la pos de son spawn (aka l'alentour ou il peut aller)
            PosGoal = new Vector3(Random.Range(StartPos.x - MaxDistanceFromSpawn / 2, StartPos.x + MaxDistanceFromSpawn / 2), Random.Range(StartPos.y - MaxDistanceFromSpawn / 2, StartPos.y + MaxDistanceFromSpawn / 2), transform.position.z);
            N += 1;      
        }
        canMove = true;
        //Debug.Log("new PosGoal = " + PosGoal);
    }

    void enableCanMove()
    {
        canMove = true;
    }

}