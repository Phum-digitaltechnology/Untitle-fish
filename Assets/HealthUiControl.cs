using System.Collections;
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
        for (int i = 0; i < 4; i++)
        {
            if (healthHolder.transform.GetChild(i).gameObject.TryGetComponent<HealthLose>(out HealthLose health))
            {
                if (health.IsLoseHealth == false)
                {
                    StartCoroutine(Delay(health));
                    break;
                }
            }
        }
    }

    IEnumerator Delay(HealthLose healthLose)
    {
        yield return new WaitForSeconds(0.1f);
        healthLose.ActiveHealthLose(OnfinishHealthLose);
    }

    void OnfinishHealthLose()
    {

    }
}

