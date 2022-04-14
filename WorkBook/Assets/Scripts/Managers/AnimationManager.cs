using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : GenericSingletonClass<AnimationManager>
{

    private static Animator animatorPage;
    public static string animatorPageGameObjectName;

    public static AnimationClip animationClip;
    public void SetAnimator()
    {
        animatorPage = GameObject.Find(animatorPageGameObjectName).GetComponent<Animator>();
        animatorPage.gameObject.SetActive(false);
    }

    public void turnLeftPage()
    {
        animatorPage.gameObject.SetActive(true);
        animatorPage.SetTrigger("turnLeft");
        Invoke("AfterTurnLeftPage", animationClip.length);
        SoundManager.PlaySound(4);

    }

    public void turnRightPage()
    {
        animatorPage.gameObject.SetActive(true);
        animatorPage.SetTrigger("turnRight");
        Invoke("AfterTurnRightPage", animationClip.length);
        SoundManager.PlaySound(4);
    }

    void AfterTurnLeftPage()
    {
        LevelManager.LoadNextLevel();
        animatorPage.gameObject.SetActive(false);
    }

    void AfterTurnRightPage()
    {
        LevelManager.LoadPreviousLevel();
        animatorPage.gameObject.SetActive(false);
    }
}
