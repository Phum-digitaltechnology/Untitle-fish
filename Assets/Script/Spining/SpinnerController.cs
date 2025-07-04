using UnityEngine;
using UnityEngine.Events;

public class SpinnerController : MonoBehaviour
{
    public Vector3 lastMouseDirection;
    float previosReelingAmount;
    public float reelingAmount;
    public int reelingMax;
    float spinSpeed;
    float spinDecay = 1f;

    [SerializeField] UnityEvent OnFinishLoop;
    [SerializeField] UnityEvent OnSuccessLoop;

    public void Update()
    {
        Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseDir = (mousePos - center).normalized;

        if (Input.GetMouseButton(0))
        {
            if (lastMouseDirection != Vector3.zero)
            {
                float angle = Vector3.SignedAngle(lastMouseDirection, mouseDir, Vector3.forward);
                reelingAmount += angle / 360;
                spinSpeed += angle * 2;
            }
            lastMouseDirection = mouseDir;
        }
        else
        {
            lastMouseDirection = Vector3.zero;
        }

        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime); // Make the object spin

        spinSpeed = Mathf.Lerp(spinSpeed, 0f, Time.deltaTime * spinDecay); // the Speed of object spin




        if (reelingAmount >= reelingMax || reelingAmount <= -reelingMax)
        {
            OnSuccessLoop.Invoke();
            reelingAmount = 0f;
        }

        if (Mathf.FloorToInt(reelingAmount) > Mathf.FloorToInt(previosReelingAmount))
        {
            OnFinishLoop?.Invoke();
            previosReelingAmount = Mathf.FloorToInt(reelingAmount);
        }

    }
}
