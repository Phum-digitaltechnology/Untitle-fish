using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;
    [SerializeField] public int Life;
    private int currentLife = 0;

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
        //check score to increase minigame speed
    }

}
