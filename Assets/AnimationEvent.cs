using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] UnityEvent DoingSomething;


    public void OnDoingSomething()
    {
        DoingSomething?.Invoke();
    }
}
