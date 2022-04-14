using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Enemy EnemyData;

    private void Awake()
    {
        if (EnemyData != null)
        {
            LoadData(EnemyData);
        }
    }   

    private void LoadData(Enemy Data)
    {
        Instantiate(Data.Modele, gameObject.transform);
        Damage = Data.Damage;
    }

    public int getDamage()
    {
        return Damage;
    }

}
