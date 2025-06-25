using UnityEngine;
using UnityEngine.Events;

public class parabolaFish : MonoBehaviour
{
    [SerializeField] UnityEvent OnCatchingFish;
    public void OnTriggerEnter2D(Collider2D col)
    {
        OnCatchingFish?.Invoke();
    }
}
