using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool invincible = true;

    public bool Invincible { get { return invincible; } set { invincible = value; } }

    private void Start()
    {
        Invoke("SetInvincible", 2f);
    }

    private void SetInvincible()
    {
        invincible = false;
    }

    public void DamagePlayer(int Damage)
    {
        if (!invincible)
        {
            PlayerManager.Instance.DamagePlayer(Damage);
        }
    }

    public void KillPlayer()
    {
        if (!invincible)
        {
            PlayerManager.Instance.KillPlayer();
        }
    }


}
