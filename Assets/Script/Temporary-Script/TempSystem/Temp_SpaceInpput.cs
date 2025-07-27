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
            
            switch (Random.Range(0, 3))
            {
                case 0:
                    AudioManager.Instance.PlaySFX("destroyV1");
                    break;
                case 1:
                    AudioManager.Instance.PlaySFX("destroyV2");
                    break;
                case 2:
                    AudioManager.Instance.PlaySFX("destroyV3");
                    break;
            }
            
            OnKeyButtonPress?.Invoke();
        }
    }
}
