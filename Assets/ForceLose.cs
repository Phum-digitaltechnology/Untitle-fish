using UnityEngine;

public class ForceLose : MonoBehaviour
{

    sceneManager sceneManager;
    ScoreSystem scoreSystem;
    PauseMenu pauseMenu;
    public void forceLose()
    {

        Debug.Log("Clicking");
        if (sceneManager == null)
        {
            sceneManager = FindAnyObjectByType<sceneManager>(FindObjectsInactive.Include);
            scoreSystem = FindAnyObjectByType<ScoreSystem>(FindObjectsInactive.Include);
            pauseMenu = FindAnyObjectByType<PauseMenu>(FindObjectsInactive.Include);
        }

        if (sceneManager.isEndMinigame)
        {
            sceneManager.StopAllCoroutines();
            scoreSystem.InstantLose();
        }
        else
        {
            sceneManager.EndMiniGame(false);
            sceneManager.OnLoadingIntoScene += waitLoadIntermission;
        }
        pauseMenu.OnBruhPlayGame();
        pauseMenu.EndPauseGame();
    }


    public void waitLoadIntermission(string name)
    {
        sceneManager.StopAllCoroutines();
        scoreSystem.InstantLose();
        sceneManager.OnLoadingIntoScene -= waitLoadIntermission;

    }

}
