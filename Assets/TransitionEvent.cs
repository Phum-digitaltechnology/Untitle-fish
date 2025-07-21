using System;
using UnityEngine;

public class TransitionEvent : MonoBehaviour
{
    public event Action waitTransitionEvent;


    public void OnTransitionEnd()
    {
        waitTransitionEvent?.Invoke();
        waitTransitionEvent = null;
    }
}
