using TMPro;
using UnityEngine;

public class FishTugMinigame : MiniGameBase
{
    [Header("Scene Refs")]
    public Transform fish;

    [Header("Gameplay Settings")]
    public float dragThreshold = 50f;
    public float swimSpeed = 3f;
    public float swimLimitX = 5f;
    public float returnSpeed = 5f;

    private int fishDir;
    private bool isDragging = false;
    private bool returningCenter = false;
    private bool inputLocked = false;
    private Vector3 dragOffset;

    private PhaseUpdateMinigameState_Caller caller;

    private void Awake()
    {
        caller = FindAnyObjectByType<PhaseUpdateMinigameState_Caller>();
        if (caller == null) Debug.LogError("[FishTug] no PhaseUpdateMinigameState_Caller found!");
    }

    private void Start()
    {
        fishDir = (Random.value < 0.5f) ? -1 : 1;
        ApplyFishRotation();

        Vector3 p = fish.position; p.y = 0f; fish.position = p;
    }

    private void Update()
    {
        if (returningCenter)
        {
            Vector3 target = new Vector3(0f, fish.position.y, fish.position.z);
            fish.position = Vector3.MoveTowards(fish.position, target, returnSpeed * Time.deltaTime);
            if (Mathf.Abs(fish.position.x) < 0.01f)
            {
                caller.UpdateStateAndGoNextPhase(true);
                Debug.Log("win from FishTugMinigame");
                returningCenter = false;
            }
            return;
        }

        if (!isDragging)
        {
            fish.position += Vector3.right * fishDir * swimSpeed * Time.deltaTime;
        }

        if (Mathf.Abs(fish.position.x) > swimLimitX)
        {
            caller.UpdateStateAndGoNextPhase(false);
            enabled = false; // หยุดสคริปต์นี้
            return;
        }

        if (inputLocked) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mw, Vector2.zero);
            if (hit && hit.transform == fish)
            {
                isDragging = true;
                dragOffset = fish.position - (Vector3)mw;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = (Vector3)mw + dragOffset;
            fish.position = new Vector3(newPos.x, fish.position.y, fish.position.z);

            if ((fishDir == -1 && fish.position.x > swimLimitX) ||
                (fishDir == 1 && fish.position.x < -swimLimitX))
            {
                isDragging = false;
                inputLocked = true;
                returningCenter = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void ApplyFishRotation()
    {
        float yRot = (fishDir == 1) ? 180f : 0f;
        fish.localRotation = Quaternion.Euler(0, yRot, 0);
    }
}
