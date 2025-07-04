using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class parabolaFish : MonoBehaviour
{
    [SerializeField] UnityEvent OnCatchingFish;
    [SerializeField] private GameObject confetti;
    public void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(spawnEffect());
    }

    IEnumerator spawnEffect()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(confetti, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        OnCatchingFish?.Invoke();
    }
}
