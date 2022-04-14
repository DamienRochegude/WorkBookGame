using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{

    private int EnemyDamage;
    private int ObstacleDamage;

    private void Start()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {

            if (gameObject.GetComponent<PlayerController>().IsDashing())
            {
                SoundManager.PlaySound(0);
                other.GetComponent<EnemyLife>().KillEnemy();
                gameObject.GetComponent<PlayerController>().ResetDash();
            }
            else
            {
                SoundManager.PlaySound(1);
                EnemyDamage = other.transform.parent.GetComponent<EnemyController>().getDamage();

                gameObject.GetComponent<PlayerHealth>().DamagePlayer(EnemyDamage);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.transform.tag == "Obstacle")
        {
            SoundManager.PlaySound(1);
            ObstacleDamage = other.transform.parent.GetComponent<ObstacleController>().getDamage();

            gameObject.GetComponent<PlayerHealth>().DamagePlayer(ObstacleDamage);
        }
    }
}
