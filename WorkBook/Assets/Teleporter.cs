using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public int layerPlayer;
    public Vector3 posTeleportation;
    public bool forNextLevel = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerPlayer)
        {
            if (forNextLevel)
            {
                LevelManager.Instance.NextLevel(posTeleportation);
            }     
            else
                LevelManager.Instance.PreviousLevel(posTeleportation);
        }
    }
}
