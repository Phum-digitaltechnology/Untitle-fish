using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Global_CountDown : MonoBehaviour
{
    public event Action OnFinishCountDownEvent;

    [SerializeField] float maxSceneTime;
    float currentTime;
    [SerializeField] UnityEvent OnFinishCountDown;

    [SerializeField] UnityEvent<float> currentTimeUpdate;


    private void Start()
    {
        currentTime = maxSceneTime;
        currentTimeUpdate?.Invoke(currentTime);
    }

    public void UpdateCurrentTime(float reducedTime)
    {
        currentTime -= reducedTime;
        currentTimeUpdate?.Invoke(currentTime);
    }


    public void StartTimer()
    {
        Debug.Log($"Remaining Time {currentTime}");
        if (currentTime > 0)
        {
            StartCoroutine(CountDownCoroutine());
        }
        else
        {
            OnFinishCountDownEvent?.Invoke();
            OnFinishCountDown?.Invoke();
        }

    }


    IEnumerator CountDownCoroutine()
    {
        for (float i = 0; i < currentTime; i += Time.deltaTime)
        {
            currentTimeUpdate?.Invoke(Time.deltaTime);
            yield return null;
        }

        OnFinishCountDownEvent?.Invoke();
        OnFinishCountDown?.Invoke();
    }
}
