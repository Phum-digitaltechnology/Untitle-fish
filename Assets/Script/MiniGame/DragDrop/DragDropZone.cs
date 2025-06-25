using UnityEngine;

public class DragDropZone : MonoBehaviour
{

    public void OnMouseDrag()
    {
        Debug.Log($"Im Draggingggg {this.gameObject.name}");
    }

    GameObject DragObject()
    {
        //Some Fuction that Return
        return null;
    }
}
