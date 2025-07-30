using UnityEngine;

public class InvertAble : MonoBehaviour
{
    RectTransform rectTransform;
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

    }

    bool invert = false;
    bool needToApply;

    public void IsInvert(bool invert)
    {
        needToApply = true;
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
            if (needToApply == false) return;
            needToApply = false;
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, -Mathf.Abs(rectTransform.localScale.y), rectTransform.localScale.z);
        }
        else
        {
            if (needToApply == false) return;
            needToApply = false;
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, Mathf.Abs(rectTransform.localScale.y), rectTransform.localScale.z);

        }
    }
}