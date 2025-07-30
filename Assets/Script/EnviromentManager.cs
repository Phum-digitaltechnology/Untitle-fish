using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnviromentManager : MonoBehaviour
{
    ScoreSystem scoreSystem;
    Enviroment activeEnviroment;
    TransitionEvent waitEndTransition;



    [SerializeField] IntermissionSceneTrigger IntermissionSceneTrigger;
    [Header("Global Active Event")]
    [SerializeField] UnityEvent activeGlobalEnviroment;
    [SerializeField] UnityEvent unActiveGlobalEnviroment;

    [Header("Enviroment List")]
    [SerializeField] List<Enviroment> winEnviroment = new List<Enviroment>();
    [SerializeField] List<Enviroment> loseEnviroment = new List<Enviroment>();

    [System.Serializable]
    private class Enviroment
    {
        [SerializeField] Camera enviromentCam;
        public Camera EnviromentCam => enviromentCam;
        [SerializeField] UnityEvent onActiveThisEnviroment;
        [SerializeField] UnityEvent offActiveEnviroment;

        public void Active()
        {
            if (enviromentCam != null) enviromentCam?.gameObject.SetActive(true);
            onActiveThisEnviroment?.Invoke();
        }

        public void UnActive()
        {
            if (enviromentCam != null) enviromentCam?.gameObject.SetActive(false);
            offActiveEnviroment?.Invoke();
        }

    }

    void unActiveAll()
    {
        foreach (var enviroment in winEnviroment)
        {
            enviroment.UnActive();
        }

        foreach (var enviroment in loseEnviroment)
        {
            enviroment.UnActive();
        }
    }

    private void Awake()
    {
        waitEndTransition = FindAnyObjectByType<TransitionEvent>();
        unActiveAll();
        scoreSystem = FindAnyObjectByType<ScoreSystem>(FindObjectsInactive.Include);
        IntermissionSceneTrigger.OnEnterIntermissionScene += OnActiveScene;
        IntermissionSceneTrigger.OnExitIntermissionScene += OffActive;
    }


    void OnActiveScene()
    {
        activeGlobalEnviroment?.Invoke();
        getActiveEnviroment();

    }

    void OffActive()
    {
        unActiveGlobalEnviroment?.Invoke();
        activeEnviroment.UnActive();

    }

    public Camera ActiveCam { get; private set; }

    void getActiveEnviroment()
    {
        if (scoreSystem.MinigameState == true)
        {
            activeEnviroment = winEnviroment[Random.Range(0, winEnviroment.Count)];
            ActiveCam = activeEnviroment.EnviromentCam;
        }
        else
        {
            activeEnviroment = loseEnviroment[Random.Range(0, winEnviroment.Count)];

        }

        activeEnviroment.Active();
    }
}
