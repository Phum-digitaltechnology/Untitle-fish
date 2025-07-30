using System;
using UnityEngine;

public class IntermissionSceneTrigger : MonoBehaviour
{
    public event Action OnEnterIntermissionScene;
    public event Action OnExitIntermissionScene;
    void Start()
    {

        FindAnyObjectByType<sceneManager>(FindObjectsInactive.Include).PreLoadingIntoScene += TriggerEnviroment;
    }

    void TriggerEnviroment(string SceneName)
    {
        if (SceneName == "IntermissionMain")
        {
            OnEnterIntermissionScene?.Invoke();
        }
        else
        {
            OnExitIntermissionScene?.Invoke();
        }
    }
}
