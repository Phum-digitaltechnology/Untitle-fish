using UnityEngine;
using UnityEngine.Events;

public class Temp_SpaceInpput : MonoBehaviour
{
    [SerializeField] KeyCode InteractKey = KeyCode.Space;
    [SerializeField] UnityEvent OnKeyButtonPress;

    void Update()
    {
        if (Input.GetKeyDown(InteractKey))
        {

            OnKeyButtonPress?.Invoke();
        }
    }
}
