using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaceScript : MonoBehaviour
{
    [SerializeField]
    private GameObject UpperHead;

    private Animator UHAnim;
    // Start is called before the first frame update
    void Start()
    {
        UHAnim = UpperHead.GetComponent<Animator>();   
    }

    public void ChangeFaceState(int i)
    {
        int FaceState = i;
        UHAnim.SetInteger("FaceState",i);
    }
}
