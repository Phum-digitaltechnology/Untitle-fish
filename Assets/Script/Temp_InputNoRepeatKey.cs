using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Temp_InputNoRepeatKey : MonoBehaviour
{
    [SerializeField] List<KeyCode> InputKey = new List<KeyCode>() { KeyCode.A, KeyCode.B };

    [SerializeField] UnityEvent OnSuccessInputKey;
    [SerializeField] UnityEvent OnFailureInputpKey;
    KeyCode previousKey;

    [Header("Key Press")]
    [SerializeField] List<UnityEvent> OnKeyPressEvent = new List<UnityEvent>();


    private void Update()
    {
        for (int i = 0; i < InputKey.Count; i++)
        {
            {
                if (Input.GetKeyDown(InputKey[i]))
                {
                    if (previousKey == InputKey[i])
                    {
                        OnFailureInputpKey?.Invoke();
                    }
                    else
                    {
                        OnKeyPressEvent[i]?.Invoke();
                        OnSuccessInputKey?.Invoke();
                        previousKey = InputKey[i];
                    }
                }
            }
        }

    }
}
