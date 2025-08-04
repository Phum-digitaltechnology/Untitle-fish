using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{ Menu, Game }

public class GameLoopHolder : MonoBehaviour
{

    public static GameLoopHolder instance;
    GameState currentState = GameState.Menu;

    [Header("Set Up")]
    [SerializeField] List<GameStateListener> gameStateListeners = new List<GameStateListener>();
    [Header("Transition Time")]
    [SerializeField] float ExitTime;
    [SerializeField] float EnterTime;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

    }


    private void Start()
    {
        SetUpState();
    }

    public void SetUpState()
    {
        foreach (GameStateListener listener in gameStateListeners)
        {
            listener.OnSetState(currentState);
        }
    }

    public void InstantSetState(GameState currentState)
    {
        this.currentState = currentState;
        GameStateListener onEnter = null, onExit = null;
        foreach (GameStateListener listener in gameStateListeners)
        {
            if (listener.IsEnter(currentState))
            {
                onEnter = listener;
            }
            else
            {
                onExit = listener;
            }
        }
        onEnter.OnSetState(currentState);
        onExit.OnSetState(currentState);


    }

    void ChangeState(GameState newState)
    {
        currentState = newState;
        GameStateListener onEnter = null, onExit = null;
        foreach (GameStateListener listener in gameStateListeners)
        {
            if (listener.IsEnter(currentState))
            {
                onEnter = listener;
            }
            else
            {
                onExit = listener;
            }
        }
        StartCoroutine(transition(onEnter, onExit));
    }


    public void ChangeToGameState()
    {
        ChangeState(GameState.Game);
    }

    public void ChangeToMenuState()
    {

        ChangeState(GameState.Menu);

    }

    IEnumerator transition(GameStateListener enter, GameStateListener exit)
    {
        exit.OnExitTransition();
        yield return new WaitForSeconds(ExitTime);
        exit.OnSetState(currentState);
        enter.OnEnterTransition();
        yield return new WaitForSeconds(EnterTime);
        enter.OnSetState(currentState);
    }


}
