using System.Collections.Generic;
using UnityEngine;

public class Aimming : MonoBehaviour
{
    [SerializeField] private List<Transform> MovePos = new List<Transform>();
    [SerializeField] private Transform fishTransform;
    [SerializeField] private float moveSpeed = 5f;


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


    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrow keys
        MoveOnXAxis(inputX);
    }

    private void MoveOnXAxis(float inputX)
    {
        Vector3 currentPos = fishTransform.position;

        // Move only on X-axis
        Vector3 newPos = currentPos + new Vector3(inputX * moveSpeed * Time.deltaTime, 0f, 0f);

        fishTransform.position = newPos;
    }
}
