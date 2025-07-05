using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LosingScreen : MonoBehaviour
{

    public TMP_Text text;
    void Update()
    {
        text.text = "Score : " + GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Manager[(int)MANAGER.ScoreSystem].GetComponent<ScoreSystem>().currentScore.ToString();
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("Return to Title Screen");
    }
}
