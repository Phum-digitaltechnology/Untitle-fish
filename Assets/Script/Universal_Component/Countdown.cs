using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Countdown : MonoBehaviour
{
    public event Action OnFinishCountDownEvent;

    [SerializeField] float countDownTime;
    [SerializeField] UnityEvent OnFinishCountDown;

    [SerializeField] UnityEvent<float> timerUpdate;

    public void BeginCountDown()
    {
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        for (float i = 0; i <= countDownTime; i += Time.deltaTime)
        {
            timerUpdate?.Invoke(Time.deltaTime);
            yield return null;
        }

        OnFinishCountDownEvent?.Invoke();
        OnFinishCountDown?.Invoke();
    }
}
