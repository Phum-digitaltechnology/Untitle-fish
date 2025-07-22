using UnityEngine;

public class InvertAble : MonoBehaviour
{
    public void IsInvert(bool invert)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (invert)
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, -rectTransform.localScale.y, rectTransform.localScale.z);
        }
        else
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, Mathf.Abs(rectTransform.localScale.y), rectTransform.localScale.z);

        }
    }
}