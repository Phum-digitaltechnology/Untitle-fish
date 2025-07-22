using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FishTugMinigame : MonoBehaviour
{
    [Header("Scene Refs")]
    public Transform fish;
    //public Collider2D winZone;
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
    private bool isHoldingClick = false;
    //private Vector3 dragOffset;
    private bool finished = false;
    private Vector3 exit;
    private bool canSwimAway = false;
    private Collider2D fishCollider;
    private SpriteRenderer fishRenderer;

    private float currentSpeed;
    private float gameTimer;
    private bool inPhaseLoop = false;
    private bool isInPhase1 = true;

    private Coroutine phaseCoroutine;
    private bool isApply = false;

    [SerializeField] UnityEvent onFishGoLeft;
    [SerializeField] UnityEvent onFishGoRight;

    private void Start()
    {
        fishDir = (Random.value < 0.5f) ? -1 : 1;

        ApplyFishRotation();
        Vector3 loseZonePos = loseZone.transform.position;
        loseZonePos.x = fish.position.x + (fishDir * 10f);
        loseZone.transform.position = loseZonePos;
        Vector3 p = fish.position; p.y = 0f; fish.position = p;
        /*if (fishDir == -1)
        {
            var temp = winZone;
            winZone = loseZone;
            loseZone = temp;
        }*/

        fishCollider = fish.GetComponent<Collider2D>();
        if (fishCollider == null)
        {
            Debug.LogError("[FishTug] Missing Collider2D on fish!");
        }
        fishRenderer = fish.GetComponent<SpriteRenderer>();
        if (fishRenderer == null)
        {
            Debug.LogError("[FishTug] Missing SpriteRenderer on fish!");
        }
        currentSpeed = swimSpeedNormal;
        gameTimer = gameDuration;
    }

    private void Update()
    {
        if (!finished)
        {
            gameTimer -= Time.deltaTime;

            Vector2 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mw, Vector2.zero);
            bool mouseOnFish = hit && hit.transform == fish;

            if (Input.GetMouseButtonDown(0) && mouseOnFish)
            {
                isHoldingClick = true;
                if (fishRenderer && grabbedFishSprite) fishRenderer.sprite = grabbedFishSprite;
                StartPhaseLoop();
            }

            if (Input.GetMouseButtonUp(0))
            {
                isHoldingClick = false;
                ResetFish();
            }

            if (inPhaseLoop && !isHoldingClick)
            {
                StopPhaseLoop();
                ResetFish();
            }

            fish.position += Vector3.right * fishDir * currentSpeed * Time.deltaTime;

            if (gameTimer > 2f && fishCollider.IsTouching(loseZone))
            {
                Finish(false);
            }

            if (gameTimer <= 2f && !finished)
            {
                if (!fishCollider.IsTouching(loseZone)) Finish(true);
                else Finish(false);
            }
        }

        if (canSwimAway)
        {
            fish.position += exit * swimSpeedNormal * Time.deltaTime;
            if (Mathf.Abs(fish.position.x) > 30f) canSwimAway = false;
        }
    }

    private void StartPhaseLoop()
    {
        if (!inPhaseLoop)
        {
            inPhaseLoop = true;
            phaseCoroutine = StartCoroutine(PhaseLoop());
        }
    }

    private void StopPhaseLoop()
    {
        if (inPhaseLoop)
        {
            inPhaseLoop = false;
            if (phaseCoroutine != null) StopCoroutine(phaseCoroutine);
        }
    }

    private IEnumerator PhaseLoop()
    {
        while (true)
        {
            if (isInPhase1)
            {
                currentSpeed = swimSpeedReduced; // Phase 1: Slow
            }
            else
            {
                currentSpeed = swimSpeedReversed; // Phase 2: Reverse
            }
            isInPhase1 = !isInPhase1;
            yield return new WaitForSeconds(1f);
        }
    }

    private void ResetFish()
    {
        currentSpeed = swimSpeedNormal;
        if (fishRenderer && normalFishSprite) fishRenderer.sprite = normalFishSprite;
    }


    private void Finish(bool win)
    {
        finished = true;
        StopPhaseLoop();

        if (isApply) return;
        isApply = true;

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