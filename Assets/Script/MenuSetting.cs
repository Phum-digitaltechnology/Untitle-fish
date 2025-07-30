using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSetting : MonoBehaviour
{
    public Slider master;
    public Slider music;
    public Slider sfx;

    [SerializeField] GameObject StartObj;
    [SerializeField] GameObject CreditObj;
    [SerializeField] GameObject ExitObj;
    [SerializeField] GameObject SettingObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        SetMasterVolume(master.value);
        SetMusicVolume(music.value);
        SetSFXVolume(sfx.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("IntermissionMain");
    }

    public void Credits()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
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

    public void ToggleSetting()
    {
        StartObj.SetActive(!StartObj.activeInHierarchy);
        CreditObj.SetActive(!CreditObj.activeInHierarchy);
        ExitObj.SetActive(!ExitObj.activeInHierarchy);
        SettingObj.SetActive(!SettingObj.activeInHierarchy);
    }
}
