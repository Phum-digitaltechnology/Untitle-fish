using UnityEngine;

public class MouseIcon : MonoBehaviour
{
    [SerializeField] CursorMode cursorMode = CursorMode.Auto;


    public void SetCursor(Texture2D mouseIcon)
    {
        Cursor.SetCursor(mouseIcon, Vector2.zero, cursorMode);
    }

    public void EnableCursor(bool isEnable)
    {
        Cursor.visible = isEnable;
    }
}
