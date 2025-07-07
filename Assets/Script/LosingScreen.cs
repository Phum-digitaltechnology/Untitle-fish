using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingScreen : MonoBehaviour
{

    public TextMeshProUGUI Losetext;
    public TextMeshProUGUI Score;
    [SerializeField] GameObject loseUi;

    public void ActiveLosingScene()
    {
        loseUi.SetActive(true);
    }
    void Update()
    {
        string disPlayText = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Manager[(int)MANAGER.ScoreSystem].GetComponent<ScoreSystem>().currentScore.ToString();
        Losetext.text = "Score : " + disPlayText;
        Score.text = disPlayText;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("Return to Title Screen");
    }
}
