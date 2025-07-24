using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthLose : MonoBehaviour
{
    [SerializeField] UnityEvent _onHealthLose;
    Action addedCallback;
    HealthUiControl healthUiControl;

    private void Awake()
    {
        healthUiControl = FindAnyObjectByType<HealthUiControl>();
    }

    public bool IsLoseHealth { get; private set; } = false;

    public void ActiveHealthLose()
    {
        IsLoseHealth = true;

        Debug.Log("Losing Health");
        _onHealthLose?.Invoke();
    }



    public void OnFinishHealthLose()
    {
        healthUiControl.OnfinishHealthLose();


    }
}
