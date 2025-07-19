using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnviromentManager : MonoBehaviour
{
    ScoreSystem scoreSystem;
    Enviroment activeEnviroment;


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
        [SerializeField] UnityEvent onActiveThisEnviroment;
        [SerializeField] UnityEvent offActiveEnviroment;
        public void Active()
        {
            enviromentCam.gameObject.SetActive(true);
            onActiveThisEnviroment?.Invoke();
        }

        public void UnActive()
        {
            enviromentCam.gameObject.SetActive(false);
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
        unActiveAll();
        scoreSystem = FindAnyObjectByType<ScoreSystem>();
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

    void getActiveEnviroment()
    {
        if (scoreSystem.MinigameState == true)
        {
            activeEnviroment = winEnviroment[Random.Range(0, winEnviroment.Count)];
        }
        else
        {
            activeEnviroment = loseEnviroment[Random.Range(0, winEnviroment.Count)];

        }
        activeEnviroment.Active();
    }
}
