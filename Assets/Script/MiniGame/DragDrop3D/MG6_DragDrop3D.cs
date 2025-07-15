using UnityEngine;
using UnityEngine.Events;

public enum BobberColor
{
    Purple,
    Pink,
    Orange,
    Blue,
    Red,
    Green,
};

public class MG6_DragDrop3D : MonoBehaviour
{
    private Vector3 MousePosition;
    [SerializeField] BobberColor bobberColor;
    [SerializeField] private UnityEvent<BobberColor> OnApplyBobber;
    [SerializeField] public bool canDragDrop = false;

    public void SetUp()
    {
        canDragDrop = true;
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    public void OnMouseDown()
    {
        if (canDragDrop)
        {
            MousePosition = Input.mousePosition - GetMousePosition();
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.freezeRotation = true;
            }
            else
            {
                rb = this.gameObject.AddComponent<Rigidbody>();
                rb.freezeRotation = true;
            }
        }
    }

    public void OnMouseDrag()
    {
        if (canDragDrop)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePosition);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Hook")
        {
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(this.gameObject.GetComponent<Rigidbody>());
                OnApplyBobber?.Invoke(bobberColor);
            }
        }
    }
}
