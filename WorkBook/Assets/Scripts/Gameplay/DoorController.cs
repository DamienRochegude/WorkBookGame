using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InventoryController>().keyAcquired == true)
        {
            gameObject.SetActive(false);
        }
    }
}