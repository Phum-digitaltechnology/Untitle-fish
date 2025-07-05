using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneManager : MonoBehaviour
{
    [SerializeField] private List<MiniGame> MiniGameScene = new List<MiniGame>();
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private MiniGame currentMiniGame;
    [SerializeField] private GameObject GameManagerObj;
    private bool losing = false;
    
    //right click on the component and click "Load All ScriptableObjects" to load all scriptable objects
    #if UNITY_EDITOR
        [ContextMenu("Load All ScriptableObjects")]
        void LoadAllInEditor()
        {
            MiniGameScene.Clear();
            string[] guids = AssetDatabase.FindAssets("t:MiniGame");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MiniGame obj = AssetDatabase.LoadAssetAtPath<MiniGame>(path);
                if (obj != null)
                    MiniGameScene.Add(obj);
            }
        }
    #endif
    
    //random minigame from the minigame pool and increase down time
    public MiniGame randomMiniGame()
    {
        int rand = 0;
        do
        {
            rand = Random.Range(0, MiniGameScene.Count);
        } while (MiniGameScene[rand].currentDownTime < MiniGameScene[rand].coolDown);

        foreach (var m in MiniGameScene)
        {
            m.currentDownTime++;
        }

        MiniGameScene[rand].currentDownTime = 0;

        currentMiniGame = MiniGameScene[rand];
        return MiniGameScene[rand];
    }
    
    //change scene
    public void ChangeScene(string SceneName)
    {
        StartCoroutine(LoadLevel(SceneName));
    }
    //play animation before and after load scene
    IEnumerator LoadLevel(string SceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        yield return op;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
        GameObject.Find("InterMissionCanvas").SetActive(false);
        GameObject.Find("IntermissionCamera").GetComponent<AudioListener>().enabled = false;
        GameObject.Find("IntermissionCamera").GetComponent<Camera>().enabled = false;
        transitionAnim.SetTrigger("Start");
    }

    //temp code to test scene changing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ChangeScene(randomMiniGame().SceneName);
        }
        losing = GameManagerObj.GetComponent<GameManager>().Manager[(int)MANAGER.ScoreSystem].GetComponent<ScoreSystem>().losing;
    }

    private void Start()
    {
        GameManagerObj = this.transform.parent.gameObject;

        ChangeScene(randomMiniGame().SceneName);
    }

    //call to unload minigame scene and return to intermission scene
    public void EndMiniGame(bool areYouWinningSon)
    {
        //ScoreSystem scoreCS = this.transform.parent.GetChild(3).gameObject.GetComponent<ScoreSystem>();
        ScoreSystem scoreCS = GameManagerObj.GetComponent<GameManager>().Manager[(int)MANAGER.ScoreSystem].GetComponent<ScoreSystem>();

        StartCoroutine(BackToIntermission());

        if (areYouWinningSon)
        {
            scoreCS.Win();
        }
        else
        {
            scoreCS.Lose();
        }

        StartCoroutine(TriggerMiniGame());
    }

    IEnumerator TriggerMiniGame()
    {
        yield return new WaitForSeconds(3);
        if (!losing)
        {
            ChangeScene(randomMiniGame().SceneName);
        }
    }

    IEnumerator BackToIntermission()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        AsyncOperation op = SceneManager.UnloadSceneAsync(currentMiniGame.SceneName);

        op.completed += (AsyncOperation o) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("IntermissionMain"));
        };

        GameObject.Find("IntermissionCamera").GetComponent<AudioListener>().enabled = true;
        GameObject.Find("IntermissionCamera").GetComponent<Camera>().enabled = true;
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "InterMissionCanvas" && !obj.activeInHierarchy)
            {
                if (!losing)
                {
                    obj.SetActive(true);
                }
                else
                {
                    obj.SetActive(true);
                    for(int i = 0; i < 4; i++)
                    {
                        obj.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    obj.transform.GetChild(4).gameObject.SetActive(true);
                }
            }
        }
        transitionAnim.SetTrigger("Start");
    }

}
