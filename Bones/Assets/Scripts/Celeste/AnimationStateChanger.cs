using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{

    public Animator animator;
    public string currentState = "Idle";

    void Start()
    {
        ChangeAnimationState(currentState);
    }

    public void ChangeAnimationState(string newState, float speed = 1) {
        animator.speed = speed;

        if (currentState == newState)
            return;

        currentState = newState;
        animator.Play(newState);
    }

}
