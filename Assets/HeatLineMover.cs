using UnityEngine;

public class HeatLineMover : MonoBehaviour
{
    public float speed = 2f;
    private bool isMoving = false;

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
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}

