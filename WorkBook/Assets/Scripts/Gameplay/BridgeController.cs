using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private GameObject[] listeBridge;

    public void SetBridges(string[] fileName)
    {
        listeBridge = GameObject.FindGameObjectsWithTag("Bridge");
    }

    public void BrigdeActivation()
    {
        for (int i = 0; i < 4; i++)
        {
            listeBridge[i].GetComponent<Renderer>().enabled = true;
            listeBridge[i].GetComponent<Collider2D>().enabled = true;
        }

    }


}
