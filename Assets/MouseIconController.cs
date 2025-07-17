using System.Collections.Generic;
using UnityEngine;
public class MouseIconController : MonoBehaviour
{
    MouseIconManage mouseIconManage;
    private void Start()
    {

        ShareComponent.instance.GetComponent<MouseIconManage>(out mouseIconManage);
        if (mouseIconManage == null)
        {
            Debug.LogError("No MouseIconManage in the Scene");
            return;
        }
    }

    [SerializeField] List<MouseIconStatus> mouseIconStatus = new List<MouseIconStatus>();

    public void ApplyMouseIcon(int index)
    {
        if (index < mouseIconStatus.Count)
        {
            Debug.LogError("Index Out Of range");
            return;
        }
        MouseIconStatus iconState = mouseIconStatus[index];
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

