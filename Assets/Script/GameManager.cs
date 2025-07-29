using System.Collections.Generic;
using UnityEngine;

enum MANAGER
{
    SceneTransition = 0,
    AudioSources = 3,
    SceneManager = 2,
    ScoreSystem = 1,
};

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public List<GameObject> Manager = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

}
