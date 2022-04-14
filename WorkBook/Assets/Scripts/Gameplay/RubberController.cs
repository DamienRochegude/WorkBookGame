using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberController : MonoBehaviour
{

    public GameObject rubber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.PlaySound(2);
            Destroy(gameObject);
            collision.GetComponent<InventoryController>().keyAcquired = true;
            rubber.SetActive(true);
        }
    }
}
