using UnityEngine;
using UnityEngine.Events;

public class FishCheck : MonoBehaviour
{
    public float radius = 0.5f;
    public LayerMask detectionLayer;
    RaycastHit hit;

    [SerializeField] UnityEvent FoundFish;
    [SerializeField] UnityEvent NotFoundFish;

    [Header("Pos Random")]
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;





    public void SetUp()
    {
        Debug.Log("Random FromSetUp");
        this.transform.position = new Vector3(Random.Range(pos1.transform.position.x, pos2.transform.position.x), this.transform.position.y, this.transform.position.z);
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, detectionLayer);
        Debug.Log($"hit lenght : {hits.Length} ");
        while (hits.Length > 0)
        {
            this.transform.position = new Vector3(Random.Range(pos1.transform.position.x, pos2.transform.position.x), this.transform.position.y, this.transform.position.z);
            hits = Physics.OverlapSphere(transform.position, radius, detectionLayer);
            Debug.Log($"Hit from While {hits.Length}");
        }
    }

    public void IsFoundFish()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, detectionLayer);

        if (hits.Length > 0)
        {
            FoundFish?.Invoke();
        }
        else
        {
            NotFoundFish?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}


