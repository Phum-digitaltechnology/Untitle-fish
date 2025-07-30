using UnityEngine;

public class LookAtTargetCam : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Transform target;
    void Update()
    {
        cam.LookAt(target);
    }
}
