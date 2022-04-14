using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{
    public GameObject pen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            PlayerManager.Instance.BrigdeActivation();
            pen.SetActive(true);
        }
    }
}
