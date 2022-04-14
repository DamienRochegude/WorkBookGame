using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Obstacle ObstacleData;

    private void Awake()
    {
        if (ObstacleData != null)
        {
            LoadData(ObstacleData);
        }
    }

    private void LoadData(Obstacle Data)
    {
        Instantiate(Data.Modele, gameObject.transform);
        Damage = Data.Damage;
    }

    public int getDamage()
    {
        return Damage;
    }
}
