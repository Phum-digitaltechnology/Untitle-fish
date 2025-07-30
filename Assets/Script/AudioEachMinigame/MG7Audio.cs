using UnityEngine;

public class MG7Audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartReelingSound()
    {
        AudioManager.Instance.PlaySFX("ReelAway");
    }

    public void PlayWinSound(bool b)
    {
        if (b)
        {
            AudioManager.Instance.PlaySFX("YAY");
        }
        else
        {
            AudioManager.Instance.PlaySFX("SadWomp");
        }
    }
}
