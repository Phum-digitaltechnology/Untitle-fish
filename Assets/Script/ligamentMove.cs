using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class ligamentMove : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] Transform destination;
    [SerializeField] float OffSetY;
    [SerializeField] Transform objToMove;
    float currentTime = 0;
    Vector3 startPos, endPos;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinishMove;
    [SerializeField] float TbeforeMove;
    [SerializeField] UnityEvent beforeFinish;
    [ContextMenu("Debug Move")]
    public void StartMove()
    {
        onStart?.Invoke();
        endPos = destination.position;
        endPos = new Vector3(destination.position.x, destination.position.y + OffSetY, destination.position.z);
        startPos = objToMove.position;
        StartCoroutine(CurveMove());
    }
    bool isplayed = false;
    IEnumerator CurveMove()
    {
        Vector3 startPos = objToMove.transform.position;
        float totalDistance = Vector3.Distance(startPos, endPos);
        float traveledDistance = 0f;

        while (traveledDistance < totalDistance)
        {
            traveledDistance += moveSpeed * Time.deltaTime;
            float t = Mathf.Clamp01(traveledDistance / totalDistance);
            Vector3 straightPos = Vector3.Lerp(startPos, endPos, t);
            Vector3 curvePos = Vector3.Slerp(startPos, endPos, t);
            straightPos.y = curvePos.y;
            objToMove.transform.position = straightPos;

            if (t >= TbeforeMove && isplayed == false)
            {
                isplayed = true;
                beforeFinish?.Invoke();
            }

            yield return null;
        }
        onFinishMove?.Invoke();
        objToMove.transform.position = endPos; // Snap to exact final position
    }
}



