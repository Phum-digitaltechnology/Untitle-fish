using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnExit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke();
    }

    // Called when the mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke();
    }
}
