using UnityEngine;

public class CreditScript : MonoBehaviour
{
    public float ScrollSpeedOriginal = 1f;
    public float ScrollSpeed = 1f;
    public float MaxScrollSpeed = 1f;
    private RectTransform rect;
    private bool startScrolling = false;
    private Vector2 originalPosition;
    private bool hasSavedOriginal = false;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        // Save original position only once for each object
        if (!hasSavedOriginal)
        {
            originalPosition = rect.anchoredPosition;
            hasSavedOriginal = true;
        }

        rect.anchoredPosition = originalPosition; // Reset position
        startScrolling = false;
        Invoke("StartScrolling", 0.3f); 
    }

    void StartScrolling()
    {
        startScrolling = true;
    }

    void Update()
    {
        if (startScrolling)
        {
            rect.anchoredPosition += new Vector2(0, ScrollSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            ScrollSpeed = MaxScrollSpeed;
        }
        else
        {
            ScrollSpeed = ScrollSpeedOriginal;
        }
        
    }
}
