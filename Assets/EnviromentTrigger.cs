using UnityEngine;

public class EnviromentTrigger : MonoBehaviour
{
    [SerializeField] GameObject Enviroment;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<sceneManager>().OnLoadingIntoScene += TriggerEnviroment;
    }

    void TriggerEnviroment(string SceneName)
    {
        if (SceneName == "IntermissionMain")
        {
            Enviroment.SetActive(true);
        }
        else
        {
            Enviroment.SetActive(false);
        }
    }
}
