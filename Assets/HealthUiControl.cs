using UnityEngine;

public class HealthUiControl : MonoBehaviour
{
    [SerializeField] Transform healthHolder;
    ScoreSystem scoreSystem;
    int previosHealth;
    private void Start()
    {
        scoreSystem = FindAnyObjectByType<ScoreSystem>();
        previosHealth = scoreSystem.Life;
        FindAnyObjectByType<sceneManager>().OnLoadingIntoScene += isEnterIntermission;
    }
    void isEnterIntermission(string sceneName)
    {
        if (sceneName == "IntermissionMain")
        {
            EnterIntermission();
        }
    }
    void EnterIntermission()
    {
        if (IsHealthChange())
        {
            previosHealth = scoreSystem.CurrentLife;
            decreaseHealth();
            if (scoreSystem.CurrentLife == 0)
                FindAnyObjectByType<LosingScreen>(FindObjectsInactive.Include).ActiveLosingScene();
            Debug.Log("Change");
        }
        else
        {
            Debug.Log("Nothing Change");
        }
    }
    bool IsHealthChange()
    {
        return previosHealth != scoreSystem.CurrentLife;
    }

    void decreaseHealth()
    {
        int missingHealth = scoreSystem.Life - scoreSystem.CurrentLife;


        for (int i = 0; i < missingHealth; i++)
        {
            healthHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}

