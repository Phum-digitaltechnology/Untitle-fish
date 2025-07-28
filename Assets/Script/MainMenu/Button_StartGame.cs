using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_StartGame : MonoBehaviour
{
    public string SceneName;
    public Slider master;
    public Slider music;
    public Slider sfx;

    private void Start()
    {
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("MusicVolume");
        sfx.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Here we gooooooo");
    }

    public void ExitGame()
    {
        //Application.Quit();
        //Debug.Log("Off we gooooooo");
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        SetMasterVolume(master.value);
        SetMusicVolume(music.value);
        SetSFXVolume(sfx.value);
    }

    public void SetMasterVolume(float t)
    {
        AudioManager.Instance.SetMasterVolume(t);
        PlayerPrefs.GetFloat("MasterVolume", t);
    }

    public void SetMusicVolume(float t)
    {
        AudioManager.Instance.SetMusicVolume(t);
        PlayerPrefs.GetFloat("MusicVolume", t);
    }

    public void SetSFXVolume(float t)
    {
        AudioManager.Instance.SetSFXVolume(t);
        PlayerPrefs.GetFloat("SFXVolume", t);
    }
}
