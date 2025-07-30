using UnityEngine;

public class IntermissionAudio : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGetFish()
    {
        AudioManager.Instance.PlaySFX("getFish");
    }

    public void PTS()
    {
        AudioManager.Instance.PlaySFX("PULLTHATSHIT");
    }
    public void BabyCrying()
    {
        AudioManager.Instance.PlaySFX("BabyCrying");
    }
}
