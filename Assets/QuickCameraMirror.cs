using UnityEngine;

public class QuickCameraMirror : MonoBehaviour
{


    void OnEnable()
    {
        this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, 180);
    }


    void OnDisable()
    {
        this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, 0);
    }
}
