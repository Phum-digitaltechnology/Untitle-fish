using UnityEngine;

public class MG4Audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playWinSound()
    {
        AudioManager.Instance.PlaySFX("YIPPEE");
    }

    public void playPullSound()
    {
        AudioManager.Instance.PlaySFXRandomPitch("CatPull");
    }
}
