using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int Life;

    public void KillEnemy()
    {
        //Lance une animation ?
        //Sleep ?
        Destroy(transform.parent.parent.gameObject);
    }
}
