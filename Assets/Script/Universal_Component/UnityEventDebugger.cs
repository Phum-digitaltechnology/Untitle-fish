using UnityEngine;

public class UnityEventDebugger : MonoBehaviour
{
    public void OnDebug(string DebugText)
    {
        Debug.Log(DebugText);
    }
}
