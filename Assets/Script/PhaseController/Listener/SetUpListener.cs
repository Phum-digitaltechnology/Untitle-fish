using System;
using UnityEngine;
using UnityEngine.Events;

public class SetUpListener : MonoBehaviour
{
    [SerializeField] UnityEvent OnSetUp;
    public event Action OnSetUpEvent;

    private void Awake()
    {
        FindAnyObjectByType<PhaseController>().OnSetUpEvent += SetUp;
    }

    void SetUp()
    {
        OnSetUp?.Invoke();
        OnSetUpEvent?.Invoke();
    }
}
