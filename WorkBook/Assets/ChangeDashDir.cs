using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDashDir : MonoBehaviour
{
    private Animator Anim;

    private void Start()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "Dash")
            {
                Anim = t.GetComponent<Animator>();
            }
        }
    }
    void ChangeDir(int i) {
        Anim.SetInteger("Dir", i);
    }
}
