using UnityEngine;

public class DragDropZone : MonoBehaviour
{
    private bool dragging = false;
    private bool haveWorm = false;
    private Vector3 offset;
    [SerializeField] private GameObject worm;

    void Update()
    {
        if (dragging)
        {
            worm.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = worm.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    private void OnMouseOver()
    {
        if (dragging)
        {
            
        }
    }
}
