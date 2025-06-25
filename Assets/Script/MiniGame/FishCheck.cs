using UnityEngine;
using UnityEngine.Events;

public class FishCheck : MonoBehaviour
{
    public float radius = 0.5f;
    public LayerMask detectionLayer;
    RaycastHit hit;

    [SerializeField] UnityEvent FoundFish;
    [SerializeField] UnityEvent NotFoundFish;


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


