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


    [ContextMenu("Test Random")]
    public void SetUp()
    {
        this.transform.position = new Vector3(Random.Range(pos1.transform.position.x, pos2.transform.position.x), this.transform.position.y, this.transform.position.z);
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


