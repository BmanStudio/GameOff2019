using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void pushAnimationTrigger()
    {
        animator.SetTrigger("Push");
        animator.ResetTrigger("Pull");
        //print("i've told him.. 2");
        //animator.ResetTrigger("Push");
    }

    public void pullAnimationTrigger()
    {
        animator.SetTrigger("Pull");
        animator.ResetTrigger("Idle");
    }

    public void IdleAnimationTrigger()
    {
        animator.SetTrigger("Idle");
        animator.ResetTrigger("Push");
        //print("i've told him..");
        //animator.ResetTrigger("Idle");
    }
}
