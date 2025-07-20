using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
