using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Temp_InputNoRepeatKey : MonoBehaviour
{
    [SerializeField] List<KeyCode> InputKey = new List<KeyCode>() { KeyCode.A, KeyCode.B };

    [SerializeField] UnityEvent OnSuccessInputKey;
    [SerializeField] UnityEvent OnFailureInputpKey;
    KeyCode previosKey;
    private void Update()
    {
        foreach (KeyCode key in InputKey)
        {
            if (Input.GetKeyDown(key))
            {
                if (previosKey == key)
                {
                    OnFailureInputpKey?.Invoke();
                }
                else
                {
                    OnSuccessInputKey?.Invoke();
                    previosKey = key;
                }
            }
        }
    }

}
