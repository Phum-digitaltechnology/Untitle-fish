using UnityEngine;
using UnityEngine.Events;

public class GameStateListener : MonoBehaviour
{

    [SerializeField] GameState state;
    [Header("Set Up State")]
    [SerializeField] UnityEvent OnActiveState;
    [SerializeField] UnityEvent OnDisActiveState;
    [Header("On Transition")]
    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnExit;

    public void OnSetState(GameState currentGameState)
    {
        if (currentGameState != state)
        {
            OnDisActiveState?.Invoke();
        }
        else
        {
            OnActiveState?.Invoke();
        }
    }

    public bool IsEnter(GameState currentState) => currentState == state;


    public void OnEnterTransition()
    {
        OnEnter?.Invoke();
    }

    public void OnExitTransition()
    {
        OnExit?.Invoke();
    }


}
