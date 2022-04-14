using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{


    [SerializeField] private Animator transition;
    [SerializeField] private GameObject Title;

    private bool camStartGameIsSet = false;

    private void Update()
    {
        if (Input.anyKey)
        {
            if (!camStartGameIsSet)
            {
                camStartGameIsSet = true;
                StartCoroutine(changeCamera());
            }


        }

    }

    IEnumerator changeCamera()
    {
        transition.SetTrigger("triggerCamera");
        yield return new WaitForSeconds(2);

        
        CameraManager.Instance.SetCam(1);
        Title.SetActive(false);

        yield return new WaitForSeconds(2);
        transition.SetTrigger("triggerCamera");




    }
}
