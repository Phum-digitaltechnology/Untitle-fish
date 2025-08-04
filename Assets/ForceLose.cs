using UnityEngine;

public class ForceLose : MonoBehaviour
{

    sceneManager sceneManager;
    PauseMenu pauseMenu;
    GameLoopHolder gameLoopHolder;
    TransitionEvent transitionEvent;
    Animator anim;
    public void forceLose()
    {
        if (sceneManager == null)
        {
            transitionEvent = FindAnyObjectByType<TransitionEvent>(FindObjectsInactive.Include);
            anim = transitionEvent.GetComponent<Animator>();
            gameLoopHolder = FindAnyObjectByType<GameLoopHolder>(FindObjectsInactive.Include);
            sceneManager = FindAnyObjectByType<sceneManager>(FindObjectsInactive.Include);
            pauseMenu = FindAnyObjectByType<PauseMenu>(FindObjectsInactive.Include);
        }

        if (sceneManager.isEndMinigame)
        {
            gameLoopHolder.ChangeToMenuState();
            anim.SetTrigger("DisableAnimation");
            sceneManager.StopAllCoroutines();
        }
        else
        {
            sceneManager.BackToIntermissionCall();
            transitionEvent.waitTransitionEvent += waitEnd;
        }
        pauseMenu.OnBruhPlayGame();
        pauseMenu.EndPauseGame();
    }


    void waitEnd()
    {
        anim.SetTrigger("DisableAnimation");
        sceneManager.StopAllCoroutines();
        gameLoopHolder.InstantSetState(GameState.Menu);
    }
}


