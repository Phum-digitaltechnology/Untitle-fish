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
    public float swimSpeedState1 = 100f;   // Angry
    public float swimSpeedState2 = 50f;    // Tired
    public float swimSpeedReversed = -100f;
    public float gameDuration = 5f;

    [Header("Callbacks")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private float gameTimer;
    private float phaseTimer = 1f;
    private int fishDir;
    private float currentSpeed;
    private bool isInState1 = true;

    private bool finished = false;
    private Collider2D fishCollider;
    private SpriteRenderer fishRenderer;
    private Vector3 exit;

    private void Start()
    {
        fishDir = (Random.value < 0.5f) ? -1 : 1;
        ApplyFishRotation();

        Vector3 loseZonePos = loseZone.transform.position;
        loseZonePos.x = fish.position.x + fishDir * 10f;
        loseZone.transform.position = loseZonePos;

        Vector3 pos = fish.position;
        pos.y = 0f;
        fish.position = pos;

        fishCollider = fish.GetComponent<Collider2D>();
        fishRenderer = fish.GetComponent<SpriteRenderer>();

        gameTimer = gameDuration;
        currentSpeed = swimSpeedState1;
    }

    private void Update()
    {
        if (finished) return;

        gameTimer -= Time.deltaTime;
        phaseTimer -= Time.deltaTime;

        // 1. Phase Switch (loop)
        if (phaseTimer <= 0f)
        {
            isInState1 = !isInState1;
            phaseTimer = 1f;
            currentSpeed = isInState1 ? swimSpeedState1 : swimSpeedState2;
        }

        // 2. Input Click
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);
        bool onFish = hit && hit.transform == fish;

        if (Input.GetMouseButtonDown(0) && onFish)
        {
            if (fishRenderer && grabbedFishSprite)
                fishRenderer.sprite = grabbedFishSprite;

            if (isInState1)
                currentSpeed = swimSpeedState2;
            else
                currentSpeed = swimSpeedReversed;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (fishRenderer && normalFishSprite)
                fishRenderer.sprite = normalFishSprite;
        }

        fish.position += Vector3.right * fishDir * currentSpeed * Time.deltaTime;

        if (gameTimer > 2f && fishCollider.IsTouching(loseZone))
        {
            Finish(false);
        }

        if (gameTimer <= 2f)
        {
            if (fishCollider.IsTouching(loseZone))
                Finish(false);
            else
                Finish(true);
        }
    }

    private void Finish(bool win)
    {
        if (finished) return;
        finished = true;

        //exit = Vector3.right * (win ? -fishDir : fishDir);

        if (win)
            StartCoroutine(InvokeWinAfterDelay());
        else
            OnLose.Invoke();

        StartCoroutine(SwimAway());
    }

    private IEnumerator InvokeWinAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        OnWin.Invoke();
    }

    private IEnumerator SwimAway()
    {
        while (Mathf.Abs(fish.position.x) < 30f)
        {
            fish.position += exit * swimSpeedState1 * Time.deltaTime;
            yield return null;
        }
    }

    private void ApplyFishRotation()
    {
        float yRot = (fishDir == 1) ? 180f : 0f;
        fish.localRotation = Quaternion.Euler(0, yRot, 0);
    }
}
