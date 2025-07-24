using System;
using UnityEngine;

public class TransitionEvent : MonoBehaviour
{
    public event Action waitTransitionEvent;

    Animator thisAnimator;
    bool isWaitToFinish;
    private void Awake()
    {
        thisAnimator = this.GetComponent<Animator>();
    }
    public void OnTransitionEnd()
    {
        if (IsAnimatorPlaying(thisAnimator))
        {
            isWaitToFinish = true;
            return;
        }
        waitTransitionEvent?.Invoke();
        waitTransitionEvent = null;
    }

    private void Update()
    {
        if (IsAnimatorPlaying(thisAnimator) == false && isWaitToFinish)
        {
            isWaitToFinish = false;
            waitTransitionEvent?.Invoke();
            waitTransitionEvent = null;
        }
    }

    bool IsAnimatorPlaying(Animator animator)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1f || animator.IsInTransition(0);
    }

}
