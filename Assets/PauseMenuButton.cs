using UnityEngine;
using UnityEngine.Events;

public class PauseMenuButton : MonoBehaviour
{
    [SerializeField] UnityEvent ResumButton, CreditButton, QuitButton;

    public void resumButton()
    {
        ResumButton.Invoke();
    }

    public void creditButton()
    {
        CreditButton.Invoke();
    }

    public void quitButton()
    {
        QuitButton.Invoke();
    }
}
