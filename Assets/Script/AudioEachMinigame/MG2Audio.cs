using UnityEngine;

public class MG2Audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playWinSound(bool b)
    {
        if (b)
        {
            AudioManager.Instance.PlaySFX("YIPPEE");
        }
        else
        {
            AudioManager.Instance.PlaySFX("SadWomp");
        }
    }
}
