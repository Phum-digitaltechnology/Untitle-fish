using UnityEngine;
using UnityEngine.Events;

public class MouseController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] MouseBoolChecker MBC;
    [SerializeField] UnityEvent OnClicking;
    [SerializeField] UnityEvent OffClicking;
    [SerializeField] float delayBeforePlay;

    private void Update()
    {
        float timer = Time.deltaTime;
        if (timer <= delayBeforePlay)
        {
            if (!MBC.win)
            {
                CursurController();
            }
            else
            {
                this.enabled = false;
            }
        }
    }

    private void CursurController()
    {
        if (Input.GetMouseButton(0))
        {
            OnClicking.Invoke();
        }
        else
        {
            OffClicking.Invoke();
        }
    }
}
