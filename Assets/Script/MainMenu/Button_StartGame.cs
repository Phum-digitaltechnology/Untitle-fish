using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_StartGame : MonoBehaviour
{
    public string SceneName;
    public Slider master;
    public Slider music;
    public Slider sfx;
    public PauseMenu pauseMenu;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("HasLaunchedBefore"))
        {
            // First time launching the game
            PlayerPrefs.SetFloat("MasterVolume", 1.0f);
            PlayerPrefs.SetFloat("MusicVolume", 1.0f);
            PlayerPrefs.SetFloat("SFXVolume", 1.0f);

            // Mark as launched
            PlayerPrefs.SetInt("HasLaunchedBefore", 1);
            PlayerPrefs.Save(); // Optional but good practice to save right away
        }

        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("MusicVolume");
        sfx.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
        //Debug.Log("Here we gooooooo");
    }

    public void ExitGame()
    {
        //Application.Quit();
        //Debug.Log("Off we gooooooo");
        pauseMenu.EndPauseGame();
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
        PlayerPrefs.SetFloat("MasterVolume", t);
    }

    public void SetMusicVolume(float t)
    {
        AudioManager.Instance.SetMusicVolume(t);
        PlayerPrefs.SetFloat("MusicVolume", t);
    }

    public void SetSFXVolume(float t)
    {
        AudioManager.Instance.SetSFXVolume(t);
        PlayerPrefs.SetFloat("SFXVolume", t);
    }
}
