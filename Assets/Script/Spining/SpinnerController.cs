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
    [SerializeField] Transform Test;

    private void Start()
    {
        AudioManager.Instance.PlaySFXLoop("Reeling");
        AudioManager.Instance.SetSFXPitch("Reeling", 0.0f);
    }

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
                if (spinSpeed < 0)
                {
                    reelingAmount += angle / 360;
                }
                spinSpeed += angle * 2;
            }
            lastMouseDirection = mouseDir;
        }
        else
        {
            lastMouseDirection = Vector3.zero;
        }

        Test.transform.Rotate(new Vector3(1, 0, 0), spinSpeed * Time.deltaTime); // Make the object spin


        spinSpeed = Mathf.Lerp(spinSpeed, 0f, Time.deltaTime * spinDecay); // the Speed of object spin

        

        if (reelingAmount >= reelingMax || reelingAmount <= -reelingMax)
        {
            OnSuccessLoop.Invoke();
            reelingAmount = 0f;
            AudioManager.Instance.SetSFXPitch("Reeling", 1.0f);
            AudioManager.Instance.StopSFX("Reeling");
            AudioManager.Instance.PlaySFX("YIPPEE");
        }
        else
        {
            AudioManager.Instance.SetSFXPitch("Reeling", Mathf.Lerp(0.0f, 1.5f, Mathf.Abs(spinSpeed) / 1000.0f));
        }

        if (Mathf.FloorToInt(-reelingAmount) > Mathf.FloorToInt(-previosReelingAmount))
        {
            OnFinishLoop?.Invoke();
            previosReelingAmount = Mathf.FloorToInt(-reelingAmount);
        }

    }
}
