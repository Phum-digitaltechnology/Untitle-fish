using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;
public class MouseIconManage : MMSingleton<MouseIconManage>
{

    [SerializeField] Image cursorImage;

    static MouseIconStatus[] mouseIconLayer = new MouseIconStatus[2];



    public void SetMouse(int layer, MouseIconStatus iconState)
    {
        Debug.Log($"Layer index {layer}");
        mouseIconLayer[layer - 1] = iconState;
        MouseIconStatus applyIcon = iconState;

        if (layer == 1) //temporary Logic
        {
            if (mouseIconLayer[0].IsEnableRealCursor == false && mouseIconLayer[0].IsEnableCursorImage == false)
            {
                Debug.Log("Apply Second Layer Mouse");
                applyIcon = mouseIconLayer[1];
            }
        }


        if (applyIcon == null)
        {
            Debug.LogWarning("No Icon Found , Apply none");
            ApplyIcon(new MouseIconStatus());
            return;
        }
        ApplyIcon(applyIcon);

    }

    void ApplyIcon(MouseIconStatus iconState)
    {
        EnableCursorImage(iconState.IsEnableCursorImage);
        EnableRealCursor(iconState.IsEnableRealCursor);
        SetSizeHeight(iconState.MouseSize.y);
        SetSizeWidth(iconState.MouseSize.x);
        SetCursorImage(iconState.MouseIcon);
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
