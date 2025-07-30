using UnityEngine;
using UnityEngine.Events;

public class OnColliderEvent : MonoBehaviour
{
    [SerializeField] UnityEvent OnColliderSomething;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlaySFX("FishFall");
        OnColliderSomething?.Invoke();
    }
}
