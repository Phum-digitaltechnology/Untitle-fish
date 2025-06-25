using UnityEngine;
using UnityEngine.Events;

public class SpinningScore : MonoBehaviour
{
    [SerializeField] int MaxScore;
    [SerializeField] int CurrentScore;
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
