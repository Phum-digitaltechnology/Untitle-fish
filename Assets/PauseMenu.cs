using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] UnityEvent OnPauseGame;
    [SerializeField] UnityEvent OnPlayGame;
    [SerializeField] UnityEvent OnUnPauseGame;
    public float CurrentTimeScale;
    public int CountDownTime;
    public Text text;
    bool IsPaused;
    bool IsDelaying;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
        {
            StartPauseGame();
            IsDelaying = false;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && IsPaused)
        {
            OnPlayGame.Invoke();
            CountDown();
            Countdown();
        }

        if (IsDelaying)
        {
            EndPauseGame();
        }
    }

    public async Task<bool> CountDown()
    {
        await Task.Delay(CountDownTime);
        return IsDelaying = true;
    }

    async Task Countdown(int seconds = 3)
    {
        float start = Time.realtimeSinceStartup;

        for (int i = seconds; i > 0; i--)
        {
            text.text = i.ToString();
            Debug.Log($"Countdown: {i}");
            await Task.Delay(1000); // Wait real 1 second (not affected by timeScale)
        }

        float elapsed = Time.realtimeSinceStartup - start;
        Debug.Log($"Actual delay time: {elapsed} seconds");
    }

    public void StartPauseGame()
    {
        CurrentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        IsPaused = true;
        OnPauseGame.Invoke();
    }
    
    public void EndPauseGame()
    {
        Time.timeScale = CurrentTimeScale;
        IsPaused = false;
        OnUnPauseGame.Invoke();
    }
}