using System;
using UnityEngine;
using UnityEngine.Events;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] UnityEvent OnEndMinigame;
    public event Action<bool> OnMinigameStateUpdate;

    private void Update()
    {
        OnMinigameStateUpdate += OnWinning;
    }

    void OnWinning(bool win)
    {
        OnEndMinigame.Invoke();
    }

    public void ChangeSprite(Sprite image)
    {
       spriteRenderer.sprite = image;
    }
}
