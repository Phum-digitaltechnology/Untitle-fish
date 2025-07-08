using UnityEngine;
using UnityEngine.Events;

public class GrillFishMinigame : MonoBehaviour
{
    public HeatLineMover lineMover;
    public Collider2D cookedZone;
    public float gameDuration = 5f;

    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private float timer;
    private bool gameStarted = false;
    private bool resultChecked = false;

    private void Start()
    {
        timer = gameDuration;
        gameStarted = true;
    }

    private void Update()
    {
        if (!gameStarted) return;

        timer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            lineMover.StartMoving();
        }
        else
        {
            lineMover.StopMoving();
        }

        if (timer <= 1f && !resultChecked)
        {
            resultChecked = true;
            CheckResult();
        }

        if (timer <= 0f)
        {
            gameStarted = false;
        }
    }

    private void CheckResult()
    {
        Collider2D lineCol = lineMover.GetComponent<Collider2D>();

        if (lineCol.IsTouching(cookedZone))
        {
            OnWin?.Invoke();
        }
        else
        {
            OnLose?.Invoke();
        }
    }
}
