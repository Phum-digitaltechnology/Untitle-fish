using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_StartGame : MonoBehaviour
{
    public string SceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Here we gooooooo");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Off we gooooooo");
    }
}
