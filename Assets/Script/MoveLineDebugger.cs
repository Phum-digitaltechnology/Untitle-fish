using UnityEngine;

public class MoveLineDebugger : MonoBehaviour
{
    [SerializeField] Transform Tracker;
    [SerializeField] LineRenderer Renderer;


    // Update is called once per frame
    void Update()
    {
        Renderer.positionCount += 1;
        Renderer.SetPosition(Renderer.positionCount - 1, Tracker.transform.position);
    }
}
