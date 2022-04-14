using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : GenericSingletonClass<CameraManager>
{
    private static GameObject[] lstGOCameras;
    private static string parentName;
    private static int indexCamActive = 0;
    public static string ParentName { set { parentName = value; } }
    public static GameObject[] LstGOCameras { set { lstGOCameras = value;  } }

    // BlackBoard
    public static GameObject BlackBoard;

    //PlayerPos
    public static GameObject PlayerPosConver;

    public void InitCameras()
    {
        GameObject parent = GameObject.Find(parentName);

        if (parent == null)
        {
            Debug.LogError("Parent Camera Not Found");
            return;
        }

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < lstGOCameras.Length; i++)
        {
            lstGOCameras[i] = Instantiate(lstGOCameras[i], parent.transform);
            lstGOCameras[i].SetActive(false);
        }
        lstGOCameras[indexCamActive].SetActive(true);
    }

    public void SetCam(int index)
    {
        if (index < lstGOCameras.Length && index >= 0 && index != indexCamActive)
        {
            lstGOCameras[index].SetActive(true);
            lstGOCameras[indexCamActive].SetActive(false);
            indexCamActive = index;
        }
    }

    public Transform getCamTransform(int index)
    {
        if (index < lstGOCameras.Length && index >= 0 && index != indexCamActive)
        {
            return lstGOCameras[index].transform;
        }
        return null;
    }

    public float getCamFOV(int index)
    {
        if (index < lstGOCameras.Length && index >= 0 && index != indexCamActive)
        {
            return lstGOCameras[index].GetComponent<Camera>().fieldOfView;
        }
        return 0;
    }
}

