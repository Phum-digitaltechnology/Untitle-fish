using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public int currentScore = 0;
    [SerializeField] public int Life;
    private int currentLife = 0;
    public int CurrentLife => currentLife;
    public bool losing = false;

    [SerializeField] UnityEvent<int> currentLifeEvent;


    private void Awake()
    {
        currentLife = Life;
    }

    public void Win()
    {
        currentScore++;
    }

    public void Lose()
    {
        currentLife--;
    }

    private void Update()
    {
        if (currentLife <= 0)
        {
            losing = true;
        }
    }

}
