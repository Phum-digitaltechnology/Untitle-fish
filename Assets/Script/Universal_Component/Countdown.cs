using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Countdown : MonoBehaviour
{
    public event Action OnFinishCountDownEvent;

    [SerializeField] float countDownTime;
    [SerializeField] UnityEvent OnFinishCountDown;
    public void BeginCountDown()
    {
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        yield return new WaitForSeconds(countDownTime);
        OnFinishCountDownEvent?.Invoke();
        OnFinishCountDown?.Invoke();
    }
}
