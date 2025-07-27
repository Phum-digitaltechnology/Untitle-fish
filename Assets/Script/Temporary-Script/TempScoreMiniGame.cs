using UnityEngine;
using UnityEngine.Events;

public class TempScoreMiniGame : MonoBehaviour
{
    [SerializeField] int MaxScore;
    int CurrentScore;
    [SerializeField] UnityEvent OnAddScore;
    [SerializeField] UnityEvent OnMaxScore;

    private void Start()
    {
        AudioManager.Instance.PlaySFX("Sleeping");
    }
    public void AddScore()
    {
        OnAddScore?.Invoke();
        CurrentScore++;
        if (CurrentScore >= MaxScore)
        {
            AudioManager.Instance.StopSFX("Sleeping");
            AudioManager.Instance.PlaySFX("AngryMeow");
            OnMaxScore?.Invoke();
        }

    }
}
