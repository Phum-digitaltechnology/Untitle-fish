using UnityEngine;
using UnityEngine.Events;

public class GrillFishMinigame : MonoBehaviour
{
    [Header("Line Control")]
    public HeatLineMover lineMover;
    public float gameDuration = 5f;

    [Header("Zone Colliders")]
    public Collider2D rawZone;
    public Collider2D mediumZone;
    public Collider2D cookedZone;
    public Collider2D burnedZone;

    [Header("Fish Sprite by Doneness")]
    public FishVisual rawFishSprite;
    public FishVisual mediumFishSprite;
    public FishVisual cookedFishSprite;
    public FishVisual burnedFishSprite;

    [Header("Fish Renderer")]
    public SpriteRenderer fishRenderer;

    [Header("Events")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private float timer;
    private bool gameStarted = false;
    private bool resultChecked = false;

    private void Start()
    {
        timer = gameDuration;
        gameStarted = true;
        Debug.Log("MiniGame Start");
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

        UpdateFishSprite();

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

    private void UpdateFishSprite()
    {
        Collider2D lineCol = lineMover.GetComponent<Collider2D>();

        if (lineCol.IsTouching(rawZone))
        {
            fishRenderer.sprite = rawFishSprite.FishSprite;
            fishRenderer.color = rawFishSprite.FishColor;
        }
        else if (lineCol.IsTouching(mediumZone))
        {
            fishRenderer.sprite = mediumFishSprite.FishSprite;
            fishRenderer.color = mediumFishSprite.FishColor;

        }
        else if (lineCol.IsTouching(cookedZone))
        {
            fishRenderer.sprite = cookedFishSprite.FishSprite;
            fishRenderer.color = cookedFishSprite.FishColor;

        }
        else if (lineCol.IsTouching(burnedZone))
        {
            fishRenderer.sprite = burnedFishSprite.FishSprite;
            fishRenderer.color = burnedFishSprite.FishColor;
        }
    }

    private void CheckResult()
    {
        Collider2D lineCol = lineMover.GetComponent<Collider2D>();

        if (lineCol.IsTouching(cookedZone))
        {
            Debug.Log("WIN: Cooked Level!");
            OnWin?.Invoke();
        }
        else
        {
            Debug.Log("LOSE: Not Cooked!");
            OnLose?.Invoke();
        }
    }
}
[System.Serializable]
public class FishVisual
{
    public Sprite FishSprite;
    public Color FishColor = Color.white;
}
