using UnityEngine;

public class MG8Audio : MonoBehaviour
{
    bool isWin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playReelSound()
    {
        AudioManager.Instance.PlaySFXLoop("Reeling");
    }

    public void stopReelSound()
    {
        AudioManager.Instance.StopSFX("Reeling");
    }

    public void playObjectDropSound()
    {
        AudioManager.Instance.PlaySFX("FishFall");
    }

    public void setWin(bool b)
    {
        isWin = b;
    }

    public void playWinSound()
    {
        if (isWin)
        {
            AudioManager.Instance.PlaySFX("YAY");
        }
        else
        {
            AudioManager.Instance.PlaySFX("SadWomp");
        }
    }
}
