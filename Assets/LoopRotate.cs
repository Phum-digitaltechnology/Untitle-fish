using System.Collections;
using UnityEngine;
public class LoopRotate : MonoBehaviour
{
    public float angleA = -45f;
    public float angleB = 45f;
    public float speed = 90f; // Degrees per second

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(RotateLoop());
    }


    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(RotateLoop());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }


    private IEnumerator RotateLoop()
    {
        bool toB = true;

        while (true)
        {
            float startZ = transform.eulerAngles.z;
            startZ = NormalizeAngle(startZ);

            float targetZ = toB ? angleB : angleA;
            toB = !toB;

            while (Mathf.Abs(NormalizeAngle(transform.eulerAngles.z) - targetZ) > 0.1f)
            {
                float currentZ = NormalizeAngle(transform.eulerAngles.z);
                float newZ = Mathf.MoveTowardsAngle(currentZ, targetZ, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, 0f, newZ);
                yield return null;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, targetZ);
        }
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
