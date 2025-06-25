using System.Collections;
using UnityEngine;
using System.Collections.Generic;

enum Manager
{
    SceneTransition,
    AudioSources,
    SceneManager,
    ScoreSystem
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
            DontDestroyOnLoad(this.gameObject);

            for(int i = 0; i < 4; i++)
            {
                Manager.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
