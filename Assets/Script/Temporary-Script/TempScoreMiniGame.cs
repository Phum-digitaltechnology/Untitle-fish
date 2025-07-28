using UnityEngine;
using UnityEngine.Events;

public class TempScoreMiniGame : MonoBehaviour
{
    [SerializeField] int MaxScore;
    int CurrentScore;
    [SerializeField] UnityEvent OnAddScore;
    [SerializeField] UnityEvent OnMaxScore;
    public void AddScore()
    {
        OnAddScore?.Invoke();
        CurrentScore++;
        if (CurrentScore >= MaxScore)
        {
            OnMaxScore?.Invoke();
        }

    }
}
