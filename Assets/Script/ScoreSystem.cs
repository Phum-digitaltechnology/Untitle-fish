using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public int currentScore = 0;
    [SerializeField] public int Life;
    private int currentLife = 0;
    public bool losing = false;

    private void Start()
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
        if(currentLife <= 0)
        {
            losing = true;
        }
    }

}
