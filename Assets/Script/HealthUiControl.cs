using System.Collections;
using UnityEngine;
public class HealthUiControl : MonoBehaviour
{
    [SerializeField] Transform healthHolder;
    [SerializeField] float healthLoseDelay = 1;
    [SerializeField] float activeLoseDelay = 2;
    [SerializeField] Animator transition;
    TransitionEvent transitionEvent;
    ScoreSystem scoreSystem;
    int previosHealth;
    private void Start()
    {
        transitionEvent = transition.GetComponent<TransitionEvent>();
        scoreSystem = FindAnyObjectByType<ScoreSystem>(FindObjectsInactive.Include);
        previosHealth = scoreSystem.Life;
        FindAnyObjectByType<sceneManager>().OnLoadingIntoScene += isEnterIntermission;
    }


    public void OnReset()
    {
        if (scoreSystem == null)
        {
            transitionEvent = transition.GetComponent<TransitionEvent>();
            scoreSystem = FindAnyObjectByType<ScoreSystem>(FindObjectsInactive.Include);
            FindAnyObjectByType<sceneManager>(FindObjectsInactive.Include).OnLoadingIntoScene += isEnterIntermission;
        }

        previosHealth = scoreSystem.Life;
        for (int i = 0; i < 4; i++)
        {
            if (healthHolder.transform.GetChild(i).gameObject.TryGetComponent<HealthLose>(out HealthLose health))
            {
                health.OnReset();
            }
        }
    }


    public void OnActiveHealthUi()
    {
        StartCoroutine(onActiveUi());
    }

    IEnumerator onActiveUi()
    {
        for (int i = 0; i < 4; i++)
        {
            if (healthHolder.transform.GetChild(i).gameObject.TryGetComponent<HealthLose>(out HealthLose health))
            {
                health.OnActiveHealth();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    void isEnterIntermission(string sceneName)
    {
        if (sceneName == "IntermissionMain")
        {
            EnterIntermission();
        }
    }




    bool IsAnimatorPlaying(Animator animator)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1f || animator.IsInTransition(0);
    }

    void EnterIntermission()
    {
        if (IsHealthChange())
        {
            previosHealth = scoreSystem.CurrentLife;

            StartCoroutine(onHealthloseDelay());
        }
        else
        {
            Debug.Log("Nothing Change");
        }
    }

    IEnumerator onHealthloseDelay()
    {
        yield return new WaitForSeconds(healthLoseDelay);
        decreaseHealth();
    }
    bool IsHealthChange()
    {
        return previosHealth != scoreSystem.CurrentLife;
    }

    void decreaseHealth()
    {
        Debug.Log("Decress Health Visual");
        for (int i = 0; i < 4; i++)
        {
            if (healthHolder.transform.GetChild(i).gameObject.TryGetComponent<HealthLose>(out HealthLose health))
            {
                if (health.IsLoseHealth == false)
                {

                    health.ActiveHealthLose();
                    break;
                }
            }
        }
    }

    public void InstantLose()
    {
        for (int i = 0; i < 4; i++)
        {
            if (healthHolder.transform.GetChild(i).gameObject.TryGetComponent<HealthLose>(out HealthLose health))
            {
                if (health.IsLoseHealth == false)
                {
                    health.ActiveHealthLose();
                }
            }
        }
    }

    public void OnfinishHealthLose()
    {
        Debug.Log("Played On Health Lose");
        if (scoreSystem.CurrentLife == 0)
        {
            StartCoroutine(delayLoseScene());
        }
    }
    IEnumerator delayLoseScene()
    {
        yield return new WaitForSeconds(activeLoseDelay);
        FindAnyObjectByType<LosingScreen>(FindObjectsInactive.Include).ActiveLosingScene();
    }

}


