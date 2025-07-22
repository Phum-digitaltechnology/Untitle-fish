using UnityEngine;
using UnityEngine.Events;

public class WaitEndTransition : MonoBehaviour
{
    [SerializeField] UnityEvent OnFinishTransition;

    private void Start()
    {
        TransitionEvent events = FindAnyObjectByType<TransitionEvent>();
        if (events == null)
        {
            onFinish();
            return;
        }
        events.waitTransitionEvent += onFinish;
    }

    void onFinish()
    {
        OnFinishTransition?.Invoke();
    }

}
