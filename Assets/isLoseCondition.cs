using UnityEngine;
using UnityEngine.Events;

public class isLoseCondition : MonoBehaviour
{
    [SerializeField] UnityEvent onNotLose;
    [SerializeField] UnityEvent onLose;

    ScoreSystem scoreSystem;
    private void Awake()
    {
        scoreSystem = FindAnyObjectByType<ScoreSystem>(FindObjectsInactive.Include);
    }

    public void Active()
    {
        if (scoreSystem == null)
        {
            scoreSystem = FindAnyObjectByType<ScoreSystem>(FindObjectsInactive.Include);
        }

        if (scoreSystem.losing)
        {
            onLose?.Invoke();
        }
        else
        {
            onNotLose?.Invoke();
        }
    }
}
