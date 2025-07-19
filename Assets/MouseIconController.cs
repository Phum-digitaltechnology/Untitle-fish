using System.Collections.Generic;
using UnityEngine;
public class MouseIconController : MonoBehaviour
{
    MouseIconManage mouseIconManage;
    private void Awake()
    {
        mouseIconManage = MouseIconManage.Instance;
    }

    [SerializeField] List<MouseIconStatus> mouseIconStatus = new List<MouseIconStatus>();

    public void ApplyMouseIcon(int index)
    {
        if (mouseIconManage == null) return;
        Debug.Log($"{index}");
        MouseIconStatus iconState = mouseIconStatus[index];
        Debug.Log($"is null {iconState == null}");
        mouseIconManage.EnableCursorImage(iconState.IsEnableCursorImage);
        mouseIconManage.EnableRealCursor(iconState.IsEnableRealCursor);
        mouseIconManage.SetSizeHeight(iconState.MouseSize.y);
        mouseIconManage.SetSizeWidth(iconState.MouseSize.x);
        mouseIconManage.SetCursorImage(iconState.MouseIcon);
    }
}
[System.Serializable]
public class MouseIconStatus
{
    public bool IsEnableRealCursor;
    public bool IsEnableCursorImage;
    public Sprite MouseIcon;
    public Vector2 MouseSize;

}

