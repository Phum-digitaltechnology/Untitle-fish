using UnityEngine;

public class MG10Audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playSleepSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDestroySound()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                AudioManager.Instance.PlaySFX("destroyV1");
                break;
            case 1:
                AudioManager.Instance.PlaySFX("destroyV2");
                break;
            case 2:
                AudioManager.Instance.PlaySFX("destroyV3");
                break;
        }
    }

    public void playSleepSound()
    {
        AudioManager.Instance.PlaySFX("Sleeping");
    }

    public void playWinSound()
    {
        AudioManager.Instance.StopSFX("Sleeping");
        AudioManager.Instance.PlaySFX("AngryMeow");
    }
}
