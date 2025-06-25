using UnityEngine;
using UnityEngine.Events;

public class FishTugMinigame : MonoBehaviour
{
    [Header("Scene Refs")]
    public Transform fish;

    [Header("Gameplay Settings")]
    public float dragThreshold = 50f;
    public float swimSpeed = 3f;
    public float swimLimitX = 5f;

    [Header("Callbacks")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private int fishDir;
    private bool isDragging = false;
    private bool inputLocked = false;
    private Vector3 dragOffset;
    private bool finished = false;


    private void Start()
    {
        fishDir = (Random.value < 0.5f) ? -1 : 1;
        ApplyFishRotation();

        Vector3 p = fish.position; p.y = 0f; fish.position = p;
    }

    private void Update()
    {
        if (finished) return;

        if (!isDragging)
        {
            fish.position += Vector3.right * fishDir * swimSpeed * Time.deltaTime;
        }

        if (Mathf.Abs(fish.position.x) > swimLimitX)
        {
            Finish(false);
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
                Finish(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void Finish(bool win)
    {
        finished = true;
        isDragging = false;

        if (win) OnWin.Invoke();
        else OnLose.Invoke();
    }

    private void ApplyFishRotation()
    {
        float yRot = (fishDir == 1) ? 180f : 0f;
        fish.localRotation = Quaternion.Euler(0, yRot, 0);
    }
}
