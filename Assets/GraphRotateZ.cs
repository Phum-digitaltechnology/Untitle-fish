using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GraphRotateZ : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float p0;
    [SerializeField] float p1;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] UnityEvent OnStartRotate;
    [SerializeField] UnityEvent OnFinishRotate;
    bool isPlayed;
    [SerializeField] bool isDisable = false;

    [SerializeField] List<float> RotationLog = new List<float>();
    [ContextMenu("Lerping Z")]
    public void LerpingRotateZ()
    {


        if (isDisable) return;
        if (isPlayed) return;
        Debug.Log($"{this.gameObject.name} Apply Effect");
        isPlayed = true;
        OnStartRotate?.Invoke();
        applyRotation(p0);
        StartCoroutine(BeginLerp());
    }
    IEnumerator BeginLerp()
    {
        RotationLog = new List<float>();
        float currentTime = 0f;
        float directionZ = p1 - p0;

        while (currentTime < duration)
        {
            float t = currentTime / duration;
            float multipyZ = moveCurve.Evaluate(t);
            float zRotate = p0 + multipyZ * directionZ;
            RotationLog.Add(zRotate);
            applyRotation(zRotate);
            currentTime += Time.deltaTime;
            yield return null;
        }
        isPlayed = false;
        // Ensure final rotation is exactly p1
        applyRotation(p1);

        // Invoke event
        OnFinishRotate?.Invoke();
    }

    void applyRotation(float zRotate)
    {
        Vector3 angles = targetTransform.transform.eulerAngles;
        targetTransform.transform.rotation = Quaternion.Euler(angles.x, angles.y, zRotate);
    }
}
