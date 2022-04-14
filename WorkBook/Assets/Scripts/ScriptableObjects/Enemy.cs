using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] int damage;
    [SerializeField] GameObject modele;

    public int Damage { get => damage; }
    public GameObject Modele { get => modele; }

}
