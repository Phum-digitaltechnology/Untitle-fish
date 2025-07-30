using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;
public class MouseIconManage : MMSingleton<MouseIconManage>
{

    [SerializeField] Image cursorImage;

    public static MouseIconStatus[] mouseIconLayer = new MouseIconStatus[3];

    bool isSetUp = false;
    protected override void Awake()
    {
        base.Awake();
        isSetUp = true;
        for (int i = 0; i < mouseIconLayer.Length; i++)
        {
            mouseIconLayer[i] = new MouseIconStatus();
        }
    }

    public void SetMouse(int layer, MouseIconStatus iconState)
    {
        if (isSetUp == false)
        {
            isSetUp = true;
            for (int i = 0; i < mouseIconLayer.Length; i++)
            {
                mouseIconLayer[i] = new MouseIconStatus();
            }
        }


        Debug.Log($"Layer index {layer}");
        if (iconState != null)
            mouseIconLayer[layer] = iconState;
        MouseIconStatus applyIcon = null;


        for (int i = 0; i < mouseIconLayer.Length; i++)
        {
            if (mouseIconLayer[i].IsEnableRealCursor == true || mouseIconLayer[i].IsEnableCursorImage == true)
            {
                applyIcon = mouseIconLayer[i];
                break;
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
