using UnityEngine;

public class DebugOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Yippe Im Active Now");
    }

    private void OnDisable()
    {
        Debug.Log("NOoo im being Disable");
    }
}
