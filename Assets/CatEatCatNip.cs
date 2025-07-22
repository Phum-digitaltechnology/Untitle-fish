using System;
using UnityEngine;
using UnityEngine.Events;

public class CatEatCatNip : MonoBehaviour
{

    Action<bool> upDateState;
    [SerializeField] UnityEvent onStart;
    public void SetUp(Action<bool> finalState)
    {
        upDateState = finalState;
        onStart?.Invoke();
    }

    public void UpdateState(bool state)
    {
        upDateState?.Invoke(state);
    }

}
