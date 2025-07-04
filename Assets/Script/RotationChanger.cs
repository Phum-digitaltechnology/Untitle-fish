using UnityEngine;

public class RotationChanger : MonoBehaviour
{
    [SerializeField] Transform changer;
    public void ChangeRotationY(float y)
    {
        Vector3 changeRotaiton = new Vector3(0, y, 0);
        changer.eulerAngles = changeRotaiton;
    }
}
