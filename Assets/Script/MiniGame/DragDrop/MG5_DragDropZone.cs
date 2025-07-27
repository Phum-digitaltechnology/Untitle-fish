using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MG5_DragDropZone : MonoBehaviour
{
    private bool dragging = false;
    private bool haveWorm = false;
    [SerializeField] private Camera mainCamera;
    private Vector3 offset;
    //[SerializeField] private GameObject worm;
    [SerializeField] private List<GameObject> worms = new List<GameObject>();
    private Transform wormTransform;
    [SerializeField] UnityEvent OnDrag;
    [SerializeField] UnityEvent OffDrag;
    private bool canDragDrop = false;
    public void Setup()
    {
        canDragDrop = true;
    }

    void Update()
    {
        if (dragging)
        {
            OnDrag?.Invoke();
            if (wormTransform != null)
            {
                wormTransform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
                //Debug.Log("Dragging");
            }
        }
        else
        {
            OffDrag?.Invoke();
        } 
    }

    private void OnMouseDown()
    {
        //offset = wormTransform.position - Camera.main.WorldToScreenPoint(Input.mousePosition);
        //dragging = true;
        //Debug.Log("Mouse down");
        AudioManager.Instance.PlaySFX("PickUpWorm");
    }

    private void OnMouseUp()
    {
        dragging = false;
        haveWorm = false;
    }

    private void OnMouseOver()
    {
        if (canDragDrop)
        {
            Debug.Log("Mouse over bucket");
            if (Input.GetMouseButtonDown(0) && haveWorm == false)
            {
                Debug.Log("Get Worm!");
                wormTransform = Instantiate(worms[Random.Range(0, worms.Count)], mainCamera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).transform;
                wormTransform.SetParent(this.transform);
                haveWorm = true;
                offset = wormTransform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
                dragging = true;
                Debug.Log("Mouse down");
            }
        }
    }
}
