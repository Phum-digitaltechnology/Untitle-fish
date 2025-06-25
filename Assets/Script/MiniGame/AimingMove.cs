using System.Collections.Generic;
using UnityEngine;

public class AimingMove : MonoBehaviour
{
    [SerializeField] private Transform fishNetTranform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject fish;


    public void SetUp()
    {
        fish.transform.localPosition = new Vector3(Random.Range(-1.5f, 6), -9, 0);
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrow keys
        MoveOnXAxis(inputX);
    }

    private void MoveOnXAxis(float inputX)
    {
        Vector3 currentPos = fishNetTranform.position;

        // Move only on X-axis
        Vector3 newPos = currentPos + new Vector3(inputX * moveSpeed * Time.deltaTime, 0f, 0f);

        if (newPos.x >= 4 && newPos.x <= 18.5f)
        {
            fishNetTranform.position = newPos;
        }
    }

    public void parabolaFish()
    {
        fish.GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(14, 18), ForceMode2D.Impulse);
        fish.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2, 4), ForceMode2D.Impulse);
        Debug.Log("Fish jump");
    }
}
