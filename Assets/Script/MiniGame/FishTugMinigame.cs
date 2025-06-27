using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class FishTugMinigame : MonoBehaviour
{
    [Header("Scene Refs")]
    public Transform fish;
    public Collider2D winZone;
    public Collider2D loseZone;

    [Header("Gameplay Settings")]
    public float dragThreshold = 50f;
    public float swimSpeed = 3f;

    [Header("Callbacks")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private int fishDir;
    private bool isDragging = false;
    private Vector3 dragOffset;
    private bool finished = false;
    private Vector3 exit;
    private bool canSwimAway = false;

    private Collider2D fishCollider;

    private void Start()
    {
        fishDir = (Random.value < 0.5f) ? -1 : 1;
        ApplyFishRotation();

        Vector3 p = fish.position; p.y = 0f; fish.position = p;
        if (fishDir == -1)
        {
            var temp = winZone;
            winZone = loseZone;
            loseZone = temp;
        }

        fishCollider = fish.GetComponent<Collider2D>();
        if (fishCollider == null)
        {
            Debug.LogError("[FishTug] Missing Collider2D on fish!");
        }
    }

    private void Update()
    {
        if (!finished)
        { 
            if (!isDragging)
            {
                fish.position += Vector3.right * fishDir * swimSpeed * Time.deltaTime;
            }

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
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
        if (canSwimAway)
        {
            fish.position += exit * swimSpeed * Time.deltaTime;

            if (Mathf.Abs(fish.position.x) > 30f)
            {
                canSwimAway = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (finished) return;

        if (other == winZone)
        {
            Finish(true);
        }
        else if (other == loseZone)
        {
            Finish(false);
        }
    }

    private void Finish(bool win)
    {
        isDragging = false;
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
