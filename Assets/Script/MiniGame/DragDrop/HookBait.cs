using UnityEngine;
using UnityEngine.Events;

public class HookBait : MonoBehaviour
{
    [SerializeField] private UnityEvent OnApplyBait;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Worm")
        {
            Destroy(col.gameObject.GetComponent<Rigidbody2D>());
            OnApplyBait?.Invoke();
        }
    }

}