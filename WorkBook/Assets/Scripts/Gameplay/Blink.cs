using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField]
    private float TimeToBlink;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("PlayBlink",0f, TimeToBlink);
    }

    // Update is called once per frame
    void PlayBlink()
    {
        anim.Play("WalkerBlinkAnil");
    }
}
