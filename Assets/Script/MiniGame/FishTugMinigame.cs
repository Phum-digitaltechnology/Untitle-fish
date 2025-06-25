using TMPro;
using UnityEngine;

public class FishTugMinigame : MiniGameBase
{
    public Transform fish;
    public TextMeshProUGUI countdownText;
    public float countdownTime = 5f;
    public float dragThreshold = 50f;
    public float swimSpeed = 3f;
    public float swimLimitX = 5f;
    public float returnSpeed = 5f;

    private float timer;
    private Vector2 dragStart;
    private int fishDir;

    private bool returningToCenter = false;
    private bool inputLocked = false;
    private bool isDragging = false;
    private Vector3 dragOffset;

    private void Start()
    {
        timer = countdownTime;

        fishDir = (Random.value < 0.5f) ? -1 : 1;
        float yRot = (fishDir == 1) ? 180f : 0f;
        fish.localRotation = Quaternion.Euler(0, yRot, 0);

        Vector3 pos = fish.position;
        pos.y = 0f;
        fish.position = pos;

        countdownText.text = Mathf.Ceil(timer).ToString();
    }

    private void Update()
    {
        if (returningToCenter)
        {
            Vector3 target = new Vector3(0f, fish.position.y, fish.position.z);
            fish.position = Vector3.MoveTowards(fish.position, target, returnSpeed * Time.deltaTime);

            if (Mathf.Abs(fish.position.x) < 0.01f)
            {
                returningToCenter = false;
                EndMiniGame(true);
            }

            return;
        }

        if (gameEnded) return;

        if (!isDragging)
        {
            Vector3 move = Vector3.right * fishDir * swimSpeed * Time.deltaTime;
            fish.position += move;
        }

        if (Mathf.Abs(fish.position.x) > swimLimitX)
        {
            EndMiniGame(false);
            StartCoroutine(UnloadAfterDelay(1f));
            return;
        }

        timer -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0f)
        {
            EndMiniGame(false);
            StartCoroutine(UnloadAfterDelay(1f));
            return;
        }

        if (!inputLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);// 2D Raycast

                if (hit.collider != null && hit.transform == fish)
                {
                    isDragging = true;
                    dragOffset = fish.position - (Vector3)mouseWorldPos;
                }
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 newPos = (Vector3)mouseWorldPos + dragOffset;
                fish.position = new Vector3(newPos.x, fish.position.y, fish.position.z); // เคลื่อนเฉพาะแกน X

                // Win condition
                if ((fishDir == -1 && fish.position.x > swimLimitX) ||
                    (fishDir == 1 && fish.position.x < -swimLimitX))
                {
                    isDragging = false;
                    inputLocked = true;
                    gameEnded = true;
                    returningToCenter = true;
                }
            }


            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }
}
