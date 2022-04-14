using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlackBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CameraManager.BlackBoard = gameObject;
    }
}
