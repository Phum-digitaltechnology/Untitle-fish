using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public int currentScore = 0;
    [SerializeField] public int Life;
    [SerializeField] private float currentTimeScale = 1f;
    private int currentLife = 0;
    public int CurrentLife => currentLife;
    public bool losing = false;
    [SerializeField] UnityEvent<int> currentLifeEvent;
    [SerializeField] bool noLifeLose;

    public bool MinigameState { get; private set; } = true;


    private void Awake()
    {
        currentLife = Life;
    }

    public void Win()
    {
        MinigameState = true;
        currentScore++;
        currentTimeScale = 1f + ((float)currentScore / 100f);
        Time.timeScale = currentTimeScale;
    }

    public void Lose()
    {
        MinigameState = false;
        if (noLifeLose) return;
        currentLife--;
        if (currentLife <= 0)
        {
            losing = true;
        }
    }


}
