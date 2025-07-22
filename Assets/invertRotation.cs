using UnityEngine;

public class invertRotation : MonoBehaviour
{
    public void IsInvert(bool isInvert)
    {
        Invert = isInvert;
    }

    bool Invert = false;
    private void Update()
    {
        if (Invert)
        {
            Vector3 angles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(angles.x, angles.y, 180);
        }
        else
        {
            Vector3 angles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(angles.x, angles.y, 0);
        }
    }
}
