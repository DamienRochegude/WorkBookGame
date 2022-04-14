using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void changeFOV()
    {

        this.GetComponent<Camera>().fieldOfView = CameraManager.Instance.getCamFOV(2);
        CameraManager.Instance.SetCam(2);
    }
}
