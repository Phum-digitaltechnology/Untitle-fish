using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishRun : MonoBehaviour
{
    [SerializeField] private List<Transform> MovePos = new List<Transform>();
    [SerializeField] private Rigidbody fishTransform;
    [SerializeField] private float moveSpeed = 5f;

    private Transform targetPoint;
    int currentDesitnation;
    [SerializeField] UnityEvent ChangeToPos1;
    [SerializeField] UnityEvent ChangeToPos2;
    [SerializeField] UnityEvent OnFishLeadToDestination;
    BoxCollider fishCol;
    bool fishStop;


    private void Awake()
    {
        fishCol = fishTransform.gameObject.GetComponent<BoxCollider>();
    }
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
            DirectionX = 1;
            fishCol.center = new Vector3(Mathf.Abs(fishCol.center.x), fishCol.center.y, fishCol.center.z);
            currentDesitnation = 1;
            ChangeToPos2?.Invoke();

        }
        else
        {
            DirectionX = -1;
            fishCol.center = new Vector3(Mathf.Abs(fishCol.center.x) * -1, fishCol.center.y, fishCol.center.z);

            currentDesitnation = 0;
            ChangeToPos1?.Invoke();
        }


        // Set fish start position (preserve Z)
        fishTransform.transform.position = new Vector3(
            fishStartPos.localPosition.x,
            fishStartPos.localPosition.y,
            fishTransform.transform.localPosition.z
        );


        setTargetPos(copyOfMovePos[0]);
    }

    void setTargetPos(Transform newPos)
    {
        targetPoint = newPos;
        Vector3 fixedTargetPos = new Vector3(
    targetPoint.localPosition.x,
    targetPoint.localPosition.y,
    fishTransform.transform.localPosition.z
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

    float DirectionX = 0;
    private void MoveToPoint(Vector3 destination)
    {
        if (fishStop) return;

        if (Vector3.Distance(fishTransform.transform.position, destination) >= 0.5f)
        {
            Vector3 currentPos = fishTransform.transform.position;
            Vector3 targetPos = new Vector3(destination.x, destination.y, currentPos.z);
            fishTransform.linearVelocity = new Vector3(DirectionX * moveSpeed, 0, 0);
        }
        else
        {
            fishStop = true;
            if (currentDesitnation == 0)
            {
                DirectionX = 1;
                fishCol.center = new Vector3(Mathf.Abs(fishCol.center.x), fishCol.center.y, fishCol.center.z);
                currentDesitnation = 1;
                OnFishLeadToDestination?.Invoke();
            }
            else
            {
                DirectionX = -1;
                fishCol.center = new Vector3(-fishCol.center.x, fishCol.center.y, fishCol.center.z);
                currentDesitnation = 0;
                OnFishLeadToDestination?.Invoke();
            }

        }
    }

    public void changeMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

}
