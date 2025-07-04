using UnityEngine;

public class SpriteFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f; // Total time to fade
    [SerializeField] private float delayBeforeFade = 0f; // Optional delay before fading starts

    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isFading = false;
    float startColor;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color.a;

        timer = 0f;

    }

    public void StartFading()
    {
        isFading = true;
    }

    void Update()
    {
        if (!isFading) return;

        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(startColor, 0f, timer / fadeDuration);

        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        if (timer >= fadeDuration)
        {
            isFading = false;
            // Optional: disable or destroy object
            // gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
}
