using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FishTugMinigame : MonoBehaviour
{
    [Header("Scene Refs")]
    public Transform fish;
    public Collider2D loseZone;

    [Header("Fish Sprites")]
    [SerializeField] private SpriteRenderer fishRenderer;
    public Sprite normalFishSprite;
    public Sprite grabbedFishSprite;

    [Header("Gameplay Settings")]
    public float swimSpeedState1 = 100f;   // Angry
    public float swimSpeedState2 = 50f;    // Tired
    public float swimSpeedReversed = -100f;
    //public float gameDuration = 5f;

    [Header("Fish & Phase Settings")]
    public float phaseTimer = 1f;
    public float currentPhaseTimer;
    private int fishDir;
    private float currentSpeed;
    private bool isInState1 = true;
    private bool finished = false;
    private Collider2D fishCollider;
    private Vector3 exit;

    [Header("Fish Lane Positions")]
    [SerializeField] private Transform[] lanePositions = new Transform[3];
    private int currentYIndex = 1;
    private float[] yPositions;
    [SerializeField] private float laneSwitchDuration = 0.2f;

    [Header("2 Fish Spawn Zone")]
    [SerializeField] private Transform LeftSpawnZone;
    [SerializeField] private Transform RightSpawnZone;

    [Header("Callbacks")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;


    private void Start()
    {
        // 1. Pick side to spawn
        bool spawnLeft = Random.value < 0.5f;
        fishDir = spawnLeft ? 1 : -1;

        // 2. Set fish position to spawn point + middle lane Y
        currentYIndex = 1; // middle lane
        Vector3 spawnPos = spawnLeft ? LeftSpawnZone.position : RightSpawnZone.position;
        spawnPos.y = lanePositions[currentYIndex].position.y;
        fish.position = spawnPos;

        // 3. Rotate fish to face movement direction
        ApplyFishRotation();

        // 4. Move lose zone far ahead in fish's path
        Vector3 loseZonePos = loseZone.transform.position;
        loseZonePos.x = fish.position.x + fishDir * 19f;
        loseZone.transform.position = loseZonePos;

        // 5. Other setup
        fishCollider = fish.GetComponent<Collider2D>();
        currentPhaseTimer = phaseTimer;
        currentSpeed = swimSpeedState1;

        // 6. Store y lane positions
        yPositions = new float[lanePositions.Length];
        for (int i = 0; i < lanePositions.Length; i++)
        {
            yPositions[i] = lanePositions[i].position.y;
        }
    }

    private void Update()
    {
        if (finished) return;

        currentPhaseTimer -= Time.deltaTime;

        if (currentPhaseTimer <= 0f)
        {
            currentPhaseTimer = phaseTimer;

            int newYIndex;
            do
            {
                newYIndex = Random.Range(0, yPositions.Length);
            } while (newYIndex == currentYIndex);
            currentYIndex = newYIndex;

            // Vector3 pos = fish.position;
            // pos.y = yPositions[currentYIndex];
            // fish.position = pos;
            StartCoroutine(SmoothMoveToLane(yPositions[currentYIndex]));
        }

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);
        bool onFish = hit && hit.transform == fish;

        if (Input.GetMouseButtonDown(0) && onFish)
        {
            //Debug.Log("Fish grabbed!");
            if (fishRenderer && grabbedFishSprite)
                fishRenderer.sprite = grabbedFishSprite;

            if (isInState1)
                currentSpeed = swimSpeedState2;
            else
                currentSpeed = swimSpeedReversed;
        }

        if (Input.GetMouseButtonUp(0) || !onFish)
        {
            if (fishRenderer && normalFishSprite)
                fishRenderer.sprite = normalFishSprite;
            currentSpeed = swimSpeedState1;
        }

        fish.position += Vector3.right * fishDir * currentSpeed * Time.deltaTime;

        if (fishCollider.IsTouching(loseZone)) //gameTimer > 2f && 
        {
            Finish(false);
        }
        else
        {
            Finish(true);
        }
    }

    private void Finish(bool win)
    {
        if (finished) return;
        finished = true;

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

    private IEnumerator SmoothMoveToLane(float targetY)
    {
        Vector3 startPos = fish.position;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

        float t = 0f;
        float height = 0.5f; // how high the arc jumps

        while (t < 1f)
        {
            t += Time.deltaTime / laneSwitchDuration;
            float curvedT = Mathf.SmoothStep(0f, 1f, t); // smoother motion
            float arc = Mathf.Sin(curvedT * Mathf.PI) * height;

            Vector3 pos = Vector3.Lerp(startPos, endPos, curvedT);
            pos.y += arc; // add the hop arc
            fish.position = pos;

            yield return null;
        }

        fish.position = endPos;
    }
}
