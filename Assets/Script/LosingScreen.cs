using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LosingScreen : MonoBehaviour
{

    public TextMeshProUGUI Losetext;
    public TextMeshProUGUI Score;
    [SerializeField] GameObject loseUi;
    [SerializeField] UnityEvent onLoseEvent;
    public void ActiveLosingScene()
    {
        onLoseEvent?.Invoke();
        Debug.Log("Im Loseeee");
        loseUi.SetActive(true);
    }
    void Update()
    {
        string disPlayText = GameObject.FindAnyObjectByType<ScoreSystem>().currentScore.ToString();
        Losetext.text = "Score : " + disPlayText;
        Score.text = disPlayText;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("Return to Title Screen");
    }
}
