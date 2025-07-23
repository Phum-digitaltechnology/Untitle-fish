using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FishTugMinigame : MonoBehaviour
{
    [Header("Scene Refs")]
    public Transform fish;
    public Collider2D loseZone;

    [Header("Fish Sprites")]
    public Sprite normalFishSprite;
    public Sprite grabbedFishSprite;

    [Header("Gameplay Settings")]
    public float swimSpeedNormal = 100f;
    public float swimSpeedReduced = 50f;
    public float swimSpeedReversed = -100f;
    public float gameDuration = 5f;

    [Header("Callbacks")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private int fishDir;
    private float gameTimer;
    private float phaseTimer = 1f;
    private float currentSpeed;

    private bool finished = false;
    private bool isInPhase1 = true;
    private bool clickedInThisPhase = false;
    private bool canSwimAway = false;

    private Collider2D fishCollider;
    private SpriteRenderer fishRenderer;
    private Vector3 exit;

    private void Start()
    {
        fishDir = (Random.value < 0.5f) ? -1 : 1;
        ApplyFishRotation();

        Vector3 loseZonePos = loseZone.transform.position;
        loseZonePos.x = fish.position.x + (fishDir * 10f);
        loseZone.transform.position = loseZonePos;

        Vector3 p = fish.position;
        p.y = 0f;
        fish.position = p;

        fishCollider = fish.GetComponent<Collider2D>();
        fishRenderer = fish.GetComponent<SpriteRenderer>();

        currentSpeed = swimSpeedNormal;
        gameTimer = gameDuration;
    }

    private void Update()
    {
        if (!finished)
        {
            gameTimer -= Time.deltaTime;
            phaseTimer -= Time.deltaTime;

            // Phase switch every 1 second
            if (phaseTimer <= 0f)
            {
                isInPhase1 = !isInPhase1;
                phaseTimer = 1f;
                clickedInThisPhase = false;
                currentSpeed = isInPhase1 ? swimSpeedNormal : swimSpeedReduced;
            }

            // Input
            Vector2 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mw, Vector2.zero);
            bool mouseOnFish = hit && hit.transform == fish;

            if (Input.GetMouseButtonDown(0) && mouseOnFish && !clickedInThisPhase)
            {
                clickedInThisPhase = true;
                if (fishRenderer && grabbedFishSprite)
                    fishRenderer.sprite = grabbedFishSprite;

                if (isInPhase1)
                    currentSpeed = swimSpeedReduced;
                else
                    currentSpeed = swimSpeedReversed;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (fishRenderer && normalFishSprite)
                    fishRenderer.sprite = normalFishSprite;
            }

            // Move fish
            fish.position += Vector3.right * fishDir * currentSpeed * Time.deltaTime;

            // Check lose condition before 2s
            if (gameTimer > 2f && fishCollider.IsTouching(loseZone))
            {
                Finish(false);
            }

            // Check win/lose at 2s
            if (gameTimer <= 2f)
            {
                if (!fishCollider.IsTouching(loseZone)) Finish(true);
                else Finish(false);
            }
        }

        // Swim away animation
        if (canSwimAway)
        {
            fish.position += exit * swimSpeedNormal * Time.deltaTime;
            if (Mathf.Abs(fish.position.x) > 30f) canSwimAway = false;
        }
    }

    private void Finish(bool win)
    {
        if (finished) return;
        finished = true;

        if (win)
        {
            exit = Vector3.right * -fishDir;
            canSwimAway = true;
            StartCoroutine(InvokeWinAfterDelay());
        }
        else
        {
            exit = Vector3.right * fishDir;
            OnLose.Invoke();
            canSwimAway = true;
        }
    }

    private IEnumerator InvokeWinAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        OnWin.Invoke();
    }

    private void ApplyFishRotation()
    {
        float yRot = (fishDir == 1) ? 180f : 0f;
        fish.localRotation = Quaternion.Euler(0, yRot, 0);
    }
}