using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthLose : MonoBehaviour
{
    [SerializeField] UnityEvent _onHealthLose;
    Action addedCallback;

    private void Start()
    {
        _onHealthLose?.Invoke();
    }
    public void ActiveHealthLose(Action callBack)
    {
        addedCallback = callBack;
        _onHealthLose?.Invoke();
    }

    public void OnFinishHealthLose()
    {
        addedCallback?.Invoke();
    }
}
