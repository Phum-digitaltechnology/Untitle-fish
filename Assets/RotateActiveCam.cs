using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class RotateActiveCam : MonoBehaviour
{
    [SerializeField] float targetZRotation = 180f; // Target Z rotation in degrees
    [SerializeField] float duration = 1f;         // Time to complete rotation
    [SerializeField] UnityEvent onFinishRotate;
    public void StartRotation()
    {
        Camera cam = FindAnyObjectByType<EnviromentManager>().ActiveCam;
        StartCoroutine(RotateZ(cam.gameObject));
    }

    private IEnumerator RotateZ(GameObject rotateObj)
    {
        Quaternion startRotation = rotateObj.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(
            rotateObj.transform.eulerAngles.x,
            rotateObj.transform.eulerAngles.y,
            targetZRotation
        );

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            rotateObj.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rotateObj.transform.rotation = endRotation; // Ensure final rotation is exact
        onFinishRotate?.Invoke();

    }
}
