using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionStates { FADEOUT, FADEIN, NULL}
public class TransitionController : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void transitionChange(TransitionStates states)
    {
        switch (states)
        {
            case TransitionStates.FADEOUT:
                animator.Play("Close");
                break;
            case TransitionStates.FADEIN:
                animator.Play("Open");
                break;
            case TransitionStates.NULL:
                break;
            default:
                break;
        }
    }
}
