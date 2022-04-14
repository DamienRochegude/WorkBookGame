using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public bool keyAcquired = false;
    public bool keyCrayonAcquired = false;

    public void Update()
    {
        if (keyCrayonAcquired)
        {
            PlayerManager.keyCrayonAcquired = true;
            SoundManager.PlaySound(6);
            PlayerManager.Instance.BrigdeActivation();
        }
        if (keyAcquired)
            PlayerManager.keyAcquired = true;
    }
}
