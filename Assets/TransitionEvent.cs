using System;
using UnityEngine;
using UnityEngine.Events;

public class TransitionEvent : MonoBehaviour
{
    public event Action waitTransitionEvent;
    public event Action waitAnotherTransition;
    public event Action TempMiddleEvent;

    Animator thisAnimator;
    bool isWaitToFinish;

    [SerializeField] UnityEvent OnStart;
    [SerializeField] UnityEvent OnFinish;

    private void Awake()
    {
        thisAnimator = this.GetComponent<Animator>();
    }
    public void OnTransitionEnd()
    {
        waitTransitionEvent?.Invoke();
        waitTransitionEvent = null;
        OnFinish?.Invoke();
        waitTransitionEvent = waitAnotherTransition;
        waitAnotherTransition = null;
    }


    public void OnStartTransition()
    {
        OnStart?.Invoke();
    }

    bool IsAnimatorPlaying(Animator animator)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1f || animator.IsInTransition(0);
    }

}
