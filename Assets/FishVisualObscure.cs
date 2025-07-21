using UnityEngine;
using UnityEngine.Events;

public class FishVisualObscure : MonoBehaviour
{
    [SerializeField] int FishHealth;

    [SerializeField] UnityEvent onFishHit;

    public void OnClick()
    {
        FishHealth--;

        if (FishHealth <= 0)
        {
            Destroy(this.gameObject);
            //Fish Died
        }
        else
        {
            onFishHit?.Invoke();
        }
    }
}
