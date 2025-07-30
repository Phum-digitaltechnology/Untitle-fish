using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] UnityEvent OnReset;
    [SerializeField] public int currentScore = 0;
    [SerializeField] public int Life;
    [SerializeField] private float currentTimeScale = 1f;
    private float currentTimeIncreasement = 0f;
    private int currentLife = 0;
    public int CurrentLife => currentLife;
    public bool losing = false;
    [SerializeField] UnityEvent OnLose;
    [SerializeField] UnityEvent<int> currentLifeEvent;
    [SerializeField] bool noLifeLose;
    public bool MinigameState { get; private set; } = true;
    public int playedMinigameCount;
    public event Action<int> OnCountUpdate;
    private void Awake()
    {
        currentLife = Life;
    }

    public void SetUp()
    {
        MinigameState = true;
        playedMinigameCount = 0;
        currentScore = 0;
        currentLife = 4;
        currentTimeIncreasement = 0;
        Time.timeScale = 1;
        losing = false;
        OnReset?.Invoke();
    }
    public void Win()
    {
        playedMinigameCount++;
        OnCountUpdate?.Invoke(playedMinigameCount);
        MinigameState = true;
        currentScore++;
    }

    public void Lose()
    {
        playedMinigameCount++;
        OnCountUpdate?.Invoke(playedMinigameCount);
        MinigameState = false;
        if (noLifeLose) return;
        currentLife--;
        if (currentLife <= 0)
        {
            OnLose?.Invoke();
            losing = true;
        }
    }

    public void CheckForSpeedUp()
    {
        if (currentScore >= 5 && currentScore % 5 == 0)
        {
            currentTimeIncreasement += 0.1f;
            currentTimeScale = 1f + currentTimeIncreasement;
            Debug.Log($"{currentTimeScale}");
            Time.timeScale = currentTimeScale;
            StartCoroutine(SpeedUpVisual());
        }
    }

    IEnumerator SpeedUpVisual()
    {
        Animator anim = GameObject.Find("IntermissionCanvas").GetComponent<Animator>();
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("SlideIn");
    }

}
