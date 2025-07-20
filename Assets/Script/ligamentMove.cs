using System.Collections;
using UnityEngine;
public class ligamentMove : MonoBehaviour
{

    [SerializeField] float moveTime;
    [SerializeField] Transform destination;
    [SerializeField] Transform objToMove;
    float currentTime = 0;
    Vector3 startPos;
    [ContextMenu("Debug Move")]
    public void StartMove()
    {
        startPos = objToMove.position;
        StartCoroutine(CurveMove());
    }
    IEnumerator CurveMove()
    {
        float t = 0;
        while (t <= 1)
        {

            Vector3 finalPos = Vector3.Lerp(objToMove.transform.position, destination.transform.position, t);
            Vector3 yPos = Vector3.Slerp(objToMove.transform.position, destination.transform.position, t);
            finalPos.y = yPos.y;
            objToMove.transform.position = finalPos;
            currentTime += Time.deltaTime;
            t = currentTime / moveTime;
            yield return null;
        }
    }


}
