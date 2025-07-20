using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthLose : MonoBehaviour
{
    [SerializeField] UnityEvent _onHealthLose;
    Action addedCallback;

    private void Start()
    {

    }
    public bool IsLoseHealth { get; private set; } = false;

    public void ActiveHealthLose(Action callBack)
    {
        IsLoseHealth = true;
        Debug.Log("Losing Health");
        _onHealthLose?.Invoke();
        addedCallback = callBack;
    }



    public void OnFinishHealthLose()
    {
        addedCallback?.Invoke();
    }
}
