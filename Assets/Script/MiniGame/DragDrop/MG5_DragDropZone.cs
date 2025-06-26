using UnityEngine;

public class MG5_DragDropZone : MonoBehaviour
{
    private bool dragging = false;
    private bool haveWorm = false;
    private Vector3 offset;
    [SerializeField] private GameObject worm;
    private Transform wormTransform;

    void Update()
    {
        if (dragging)
        {
            if (wormTransform != null)
            {
                wormTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                //Debug.Log("Dragging");
            }
        }
    }

    private void OnMouseDown()
    {
        //offset = wormTransform.position - Camera.main.WorldToScreenPoint(Input.mousePosition);
        //dragging = true;
        //Debug.Log("Mouse down");
    }

    private void OnMouseUp()
    {
        dragging = false;
        haveWorm = false;
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over bucket");
        if (Input.GetMouseButtonDown(0) && haveWorm == false)
        {
            wormTransform = Instantiate(worm, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).transform;
            wormTransform.SetParent(this.transform);
            haveWorm = true;
            offset = wormTransform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
            dragging = true;
            Debug.Log("Mouse down");
        }
    }
}
