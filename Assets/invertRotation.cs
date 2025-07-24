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
            if (this.transform.rotation.z == 180) return;

            Vector3 angles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(angles.x, angles.y, 180);
        }
        else
        {
            if (this.transform.rotation.z == 0) return;

            Vector3 angles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(angles.x, angles.y, 0);
        }
    }
}
