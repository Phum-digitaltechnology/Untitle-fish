using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    [SerializeField] private List<MiniGame> MiniGameScene = new List<MiniGame>();
    [SerializeField] private List<MiniGame> CanAppear = new List<MiniGame>();
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image controlTransitionImage;
    [SerializeField] private TextMeshProUGUI tmproTransition;
    [SerializeField] private MiniGame CurrentMinigame;
    [SerializeField] private GameObject GameManagerObj;
    private bool losing = false;
    private bool SceneLoaded = false;
    public event Action<string> OnLoadingIntoScene;
    public event Action<string> PreLoadingIntoScene;
    TransitionEvent transitionEvent;
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
        CanAppear.Clear();
        foreach (var game in MiniGameScene)
        {
            if (game.weight <= 0)
            {
                game.CanAppear = false;
            }
            else
            {
                game.CanAppear = true;
            }
        }

        int total = 0;
        foreach (var game in MiniGameScene)
        {
            if (game.CanAppear)
            {
                CanAppear.Add(game);
                total += game.weight;
            }
        }

        int random = UnityEngine.Random.Range(1, total);

        int cursor = 0;
        for (int i = 0; i < CanAppear.Count; i++)
        {
            cursor += CanAppear[i].weight;
            if (cursor >= random)
            {
                MiniGame miniGame = CanAppear[i];
                miniGame.weight = 0;
                miniGame.CurrentDownTime = 0;
                return miniGame;
            }
        }
        return null;
    }

    //change scene
    public void ChangeScene(string SceneName)
    {
        if (!SceneLoaded)
        {
            Debug.Log($"Loading Into Scene {SceneName}");
            StartCoroutine(LoadLevel(SceneName));
            SceneLoaded = true;
        }
    }
    //play animation before and after load scene
    IEnumerator LoadLevel(string SceneName)
    {
        transitionAnim.SetTrigger("EndMinigame");
        yield return new WaitForSeconds(2.25f);
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        yield return op;
        PreLoadingIntoScene?.Invoke(SceneName);
        OnLoadingIntoScene?.Invoke(SceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
        GameObject.Find("InterMissionCanvas").SetActive(false);
        //transitionAnim.SetTrigger("StartMinigame");
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

        ResetWeight();
        transitionEvent = transitionAnim.GetComponent<TransitionEvent>();
        GameManagerObj = this.transform.parent.gameObject;
        OnLoadingIntoScene?.Invoke("IntermissionMain"); // trigger the Loading scene Event
        PreLoadingIntoScene?.Invoke("IntermissionMain");
        StartCoroutine(TriggerMiniGame());
    }

    private void ResetWeight()
    {
        foreach (MiniGame m in MiniGameScene)
        {
            m.weight = 10;
        }
    }

    //call to unload minigame scene and return to intermission scene
    public void EndMiniGame(bool areYouWinningSon)
    {
        //ScoreSystem scoreCS = this.transform.parent.GetChild(3).gameObject.GetComponent<ScoreSystem>();
        ScoreSystem scoreCS = GameManagerObj.GetComponent<GameManager>().Manager[(int)MANAGER.ScoreSystem].GetComponent<ScoreSystem>();
        if (areYouWinningSon)
        {
            scoreCS.Win();
        }
        else
        {
            scoreCS.Lose();
        }

        StartCoroutine(BackToIntermission());
        SceneLoaded = false;

        foreach (var game in MiniGameScene)
        {
            if (game.CurrentDownTime == 0)
            {
                game.CurrentDownTime++;
            }
            else
            {
                game.weight += 10;
                game.CurrentDownTime++;
            }
        }

        scoreCS.CheckForSpeedUp();

        StartCoroutine(TriggerMiniGame());
    }

    IEnumerator TriggerMiniGame()
    {

        yield return new WaitForSeconds(5); // maybe random this time???
        if (!losing)
        {
            CurrentMinigame = randomMiniGame();
            controlTransitionImage.sprite = CurrentMinigame.controlSprite;
            tmproTransition.text = CurrentMinigame.transitionText;
            yield return new WaitForSeconds(1);
            ChangeScene(CurrentMinigame.SceneName);
        }
    }

    bool isLoadIntermissionComplete;
    IEnumerator BackToIntermission()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        Debug.Log($"Current Minigame {Time.frameCount} {CurrentMinigame.SceneName}");
        AsyncOperation op = SceneManager.UnloadSceneAsync(CurrentMinigame.SceneName);


        op.completed += (AsyncOperation o) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("IntermissionMain"));
        };
        transitionAnim.SetTrigger("Start");
        PreLoadingIntoScene?.Invoke("IntermissionMain");
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
                }
            }
        }
        transitionEvent.waitTransitionEvent += waitTransiitonToIntermission;

    }


    void waitTransiitonToIntermission()
    {
        OnLoadingIntoScene?.Invoke("IntermissionMain");

    }
    bool IsAnimatorPlaying(Animator animator)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1f || animator.IsInTransition(0);
    }


}
