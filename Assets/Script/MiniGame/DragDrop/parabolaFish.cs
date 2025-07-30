using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class parabolaFish : MonoBehaviour
{
    [SerializeField] UnityEvent OnCatchingFish;
    [SerializeField] private GameObject confetti;
    public void OnTriggerEnter2D(Collider2D col)
    {
        AudioManager.Instance.PlaySFX("YAY");
        StartCoroutine(spawnEffect());
    }

    private void Start()
    {
        AudioManager.Instance.PlaySFX("FishJump");
    }

    IEnumerator spawnEffect()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(confetti, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        OnCatchingFish?.Invoke();
    }

    private void Update()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        if (rb.linearVelocity != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.forward);
        }
    }
}
