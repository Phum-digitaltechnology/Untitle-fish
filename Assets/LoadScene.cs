using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void LoadInToScene(string loadScene)
    {
        if (SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }

        SceneManager.LoadScene(SceneName);
    }
}
