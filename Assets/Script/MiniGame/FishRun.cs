using System.Collections.Generic;
using UnityEngine;

public class FishRun : MonoBehaviour
{
    [SerializeField] private List<Transform> MovePos = new List<Transform>();
    [SerializeField] private Transform fishTransform;
    [SerializeField] private float moveSpeed = 5f;

    private Transform targetPoint;

    public void SetUp()
    {
        if (MovePos.Count < 2)
        {
            Debug.LogWarning("MovePos list needs at least 2 points.");
            return;
        }

        int randIndex = Random.Range(0, MovePos.Count);
        Transform fishStartPos = MovePos[randIndex];
        MovePos.RemoveAt(randIndex);

        // Set fish start position (preserve Z)
        fishTransform.position = new Vector3(
            fishStartPos.localPosition.x,
            fishStartPos.localPosition.y,
            fishTransform.localPosition.z
        );

        // Set first remaining point as target
        targetPoint = MovePos[0];

        // Flatten target Z to match fish
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
        Vector3 currentPos = fishTransform.position;

        // Only move in X and Y, keep Z the same
        Vector3 targetPos = new Vector3(destination.x, destination.y, currentPos.z);

        fishTransform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);
    }
}
