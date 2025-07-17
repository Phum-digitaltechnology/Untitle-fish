using UnityEngine;
using UnityEngine.UI;
public class MouseIconManage : MonoBehaviour
{
    [SerializeField] Image cursorImage;
    private void Awake()
    {
        ShareComponent.instance.AddingComponent(this);
    }
    public void SetCursorImage(Sprite mouseIcon)
    {
        cursorImage.sprite = mouseIcon;
    }
    public void EnableCursorImage(bool isEnable)
    {
        cursorImage.enabled = isEnable;
    }

    public void EnableRealCursor(bool isEnable)
    {
        Cursor.visible = isEnable;
    }
    public void SetSizeHeight(float height)
    {
        cursorImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    public void SetSizeWidth(float width)
    {
        cursorImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

}
