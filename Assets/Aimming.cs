using System.Collections.Generic;
using UnityEngine;

public class Aimming : MonoBehaviour
{
    [SerializeField] private List<Transform> MovePos = new List<Transform>();
    [SerializeField] private Transform fishTransform;
    [SerializeField] private float moveSpeed = 5f;



    public void SetUp()
    {
        int randIndex = Random.Range(0, MovePos.Count);
        Vector3 fishStartPos = new Vector3(Random.Range(MovePos[0].transform.position.x, MovePos[1].transform.position.x), fishTransform.position.y, fishTransform.position.z);

        // Set fish start position (preserve Z)
        fishTransform.position = new Vector3(
            fishStartPos.x,
            fishStartPos.y,
            fishTransform.transform.position.z
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
        if (newPos.x > MovePos[0].position.x && newPos.x < MovePos[1].position.x)
        {
            fishTransform.position = newPos;
        }
    }
}
