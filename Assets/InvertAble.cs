using UnityEngine;

public class InvertAble : MonoBehaviour
{
    RectTransform rectTransform;
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

    }

    bool invert = false;
    public void IsInvert(bool invert)
    {
        this.invert = invert;
    }

    private void Update()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }


        if (invert)
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, -Mathf.Abs(rectTransform.localScale.y), rectTransform.localScale.z);
        }
        else
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, Mathf.Abs(rectTransform.localScale.y), rectTransform.localScale.z);

        }
    }
}