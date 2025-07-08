using UnityEngine;

public class HeatLineMover : MonoBehaviour
{
    public float speed = 2f;
    private bool isMoving = false;
    public Transform rightEdge;

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

            if (newPosition.x <= rightEdge.position.x)
            {
                transform.position = newPosition;
            }
            else
            {
                transform.position = new Vector3(rightEdge.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}

