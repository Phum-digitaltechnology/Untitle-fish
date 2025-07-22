using UnityEngine;
using UnityEngine.Events;

public class WaitEndTransition : MonoBehaviour
{
    [SerializeField] UnityEvent OnFinishTransition;

    private void Awake()
    {
        FindAnyObjectByType<TransitionEvent>().waitTransitionEvent += onFinish;
    }

    void onFinish()
    {
        OnFinishTransition?.Invoke();
    }

}
