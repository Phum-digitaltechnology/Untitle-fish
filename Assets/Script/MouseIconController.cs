using System.Collections.Generic;
using UnityEngine;
public class MouseIconController : MonoBehaviour
{
    [SerializeField] int MouseIconlayer = 1;


    [SerializeField] List<MouseIconStatus> mouseIconStatus = new List<MouseIconStatus>();

    public void ApplyMouseIcon(int index)
    {
        MouseIconStatus iconState = mouseIconStatus[index];
        MouseIconManage.Instance.SetMouse(MouseIconlayer, iconState);
    }

    MouseIconStatus previosIcon;

    public void ApplyOnTopPreviosMouseIcon(int index)
    {
        previosIcon = MouseIconManage.mouseIconLayer[MouseIconlayer - 1];
        MouseIconStatus iconState = mouseIconStatus[index];
        MouseIconManage.Instance.SetMouse(MouseIconlayer, iconState);

    }

    public void RevertMouseIcon()
    {
        MouseIconManage.Instance.SetMouse(MouseIconlayer, previosIcon);
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

