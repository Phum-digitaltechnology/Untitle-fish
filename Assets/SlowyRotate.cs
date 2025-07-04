using UnityEngine;

public class SlowyRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // Degrees per second

    void Update()
    {
        // Rotate around the Z axis slowly
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
