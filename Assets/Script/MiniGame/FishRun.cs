using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishRun : MonoBehaviour
{
    [SerializeField] private List<Transform> MovePos = new List<Transform>();
    [SerializeField] private Transform fishTransform;
    [SerializeField] private float moveSpeed = 5f;

    private Transform targetPoint;
    int currentDesitnation;
    [SerializeField] UnityEvent ChangeToPos1;
    [SerializeField] UnityEvent ChangeToPos2;

    bool fishStop;

    public void SetFishToStop()
    {
        fishStop = true;
    }

    [ContextMenu("Random")]

    public void SetUp()
    {
        if (MovePos.Count < 2)
        {
            Debug.LogWarning("MovePos list needs at least 2 points.");
            return;
        }
        List<Transform> copyOfMovePos = new List<Transform>(MovePos);


        int randIndex = Random.Range(0, MovePos.Count);
        Transform fishStartPos = copyOfMovePos[randIndex];
        copyOfMovePos.RemoveAt(randIndex);

        if (randIndex == 0)
        {
            currentDesitnation = 1;
            ChangeToPos2?.Invoke();

        }
        else
        {
            currentDesitnation = 0;
            ChangeToPos1?.Invoke();
        }


        // Set fish start position (preserve Z)
        fishTransform.position = new Vector3(
            fishStartPos.localPosition.x,
            fishStartPos.localPosition.y,
            fishTransform.localPosition.z
        );


        setTargetPos(copyOfMovePos[0]);
    }

    void setTargetPos(Transform newPos)
    {
        targetPoint = newPos;
        Vector3 fixedTargetPos = new Vector3(
    targetPoint.localPosition.x,
    targetPoint.localPosition.y,
    fishTransform.localPosition.z
                             );
        targetPoint.position = fixedTargetPos;
    }


    private void Update()
    {
        if (targetPoint != null)
        {
            MoveToPoint(targetPoint.position);
        }
    }

    private void MoveToPoint(Vector3 destination)
    {

        if (fishTransform.transform.position != destination)
        {
            Vector3 currentPos = fishTransform.position;
            // Only move in X and Y, keep Z the same
            Vector3 targetPos = new Vector3(destination.x, destination.y, currentPos.z);

            fishTransform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (fishStop) return;

            if (currentDesitnation == 0)
            {
                currentDesitnation = 1;
                ChangeToPos2?.Invoke();
                setTargetPos(MovePos[1]);
            }
            else
            {
                currentDesitnation = 0;
                ChangeToPos1?.Invoke();
                setTargetPos(MovePos[0]);
            }

        }
    }


}
