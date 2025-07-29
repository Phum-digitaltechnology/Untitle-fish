using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthLose : MonoBehaviour
{
    [SerializeField] UnityEvent onResetEvent;
    [SerializeField] UnityEvent _onHealthLose;
    Action addedCallback;
    HealthUiControl healthUiControl;

    private void Awake()
    {
        healthUiControl = FindAnyObjectByType<HealthUiControl>();
    }

    [ContextMenu("On Reset")]
    public void OnReset()
    {
        onResetEvent?.Invoke();
        IsLoseHealth = false;
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).transform.position = this.transform.position;
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
