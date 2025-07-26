using UnityEngine;

public class AimingMove : MonoBehaviour
{
    [SerializeField] private Transform fishNetTranform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject fishSpawnPoint;
    [SerializeField] Transform minMove, maxMove;

    public void SetUp()
    {
        fish.transform.localPosition = new Vector3(Random.Range(fishSpawnPoint.transform.localPosition.x - 1, fishSpawnPoint.transform.localPosition.x + 1), fishSpawnPoint.transform.localPosition.y, fishSpawnPoint.transform.localPosition.z);
        this.gameObject.GetComponent<AimingMove>().enabled = true;
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
        if (newPos.x <= minMove.position.x || newPos.x >= maxMove.position.x)
        {
            Debug.Log("Out of Range");
            return;
        }

        fishNetTranform.position = newPos;
    }

    public void parabolaFish()
    {
        fish.SetActive(true);
        fish.GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(8, 10), ForceMode2D.Impulse);
        fish.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2, 4), ForceMode2D.Impulse);
        Debug.Log("Fish jump");
    }
}
