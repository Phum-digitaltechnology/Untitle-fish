using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {
        AudioManager.Instance.PlaySFX("Hover");
    }

    public void playClickSound()
    {
        AudioManager.Instance.PlaySFX("Click");
    }
}
