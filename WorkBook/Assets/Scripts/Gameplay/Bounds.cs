using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!other.GetComponent<PlayerHealth>().Invincible)
            {
                other.GetComponent<PlayerHealth>().KillPlayer();
            }
            
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyLife>().KillEnemy();
        }

    }
}
