using UnityEngine;
using UnityEngine.Events;

public enum BobberColor
{
    Red,
    Purple,
    Blue,
    LightBlue,
    Green,
    Yellow,
};

public class MG6_DragDrop3D : MonoBehaviour
{
    private Vector3 MousePosition;
    [SerializeField] BobberColor bobberColor;
    [SerializeField] private UnityEvent<BobberColor> OnApplyBobber;
    [SerializeField] public bool canDragDrop = false;
    private bool isCollided = false;
    [SerializeField] private UnityEvent onMouseDrag;
    [SerializeField] private UnityEvent onMouseDrop;

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

            rb.linearDamping = 15f;
        }
    }

    public void OnMouseDrag()
    {
        if (canDragDrop)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePosition);
            onMouseDrag?.Invoke();
        }
    }

    public void OnMouseDrop()
    {
        onMouseDrop?.Invoke();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hook")
        {
            isCollided = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        isCollided = false;
    }

    private void Update()
    {
        if ((isCollided))
        {
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(this.gameObject.GetComponent<Rigidbody>());
                OnApplyBobber?.Invoke(bobberColor);
                Destroy(this.gameObject);
            }
        }
    }
}
