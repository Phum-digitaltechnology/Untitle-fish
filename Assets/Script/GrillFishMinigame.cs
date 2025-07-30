using UnityEngine;
using UnityEngine.Events;

public class GrillFishMinigame : MonoBehaviour
{
    [Header("Line Control")]
    public HeatLineMover lineMover;

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

    private bool gameStarted = false;
    private bool resultChecked = false;
    private bool hasPressedSpace = false;
    private bool hasReleasedSpace = false;

    [SerializeField] private GameObject smokeParticle;

    private void Start()
    {
        gameStarted = true;
        Debug.Log("MiniGame Start");
        lineMover = FindAnyObjectByType<HeatLineMover>();
    }

    private void Update()
    {
        if (!gameStarted) return;


        if (Input.GetKey(KeyCode.Space) && !hasPressedSpace)
        {
            hasPressedSpace = true;
            AudioManager.Instance.PlaySFXLoop("Frying");
            lineMover.StartMoving();
        }
        if (hasPressedSpace && !hasReleasedSpace && Input.GetKeyUp(KeyCode.Space))
        {
            hasReleasedSpace = true;
            AudioManager.Instance.StopSFX("Frying");
            lineMover.StopMoving();
            CheckResult();
        }
        UpdateFishSprite();
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
            smokeParticle.SetActive(true);
        }
    }

    private void CheckResult()
    {
        Collider2D lineCol = lineMover.GetComponent<Collider2D>();

        if (lineCol.IsTouching(cookedZone))
        {
            Debug.Log("WIN: Cooked Level!");
            AudioManager.Instance.PlaySFX("YIPPEE");
            OnWin?.Invoke();
        }
        else
        {
            Debug.Log("LOSE: Not Cooked!");
            AudioManager.Instance.PlaySFX("SadWomp");
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
