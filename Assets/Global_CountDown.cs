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


    public void ClampCurrentTime(float ClampTime)
    {
        if (currentTime > ClampTime)
        {
            currentTime = ClampTime;
        }
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
        while (currentTime > 0)
        {
            currentTimeUpdate?.Invoke(currentTime);
            currentTime -= Time.deltaTime;
            yield return null;
        }
        currentTime = -1;
        currentTimeUpdate?.Invoke(currentTime);

        OnFinishCountDownEvent?.Invoke();
        OnFinishCountDown?.Invoke();
    }
}
