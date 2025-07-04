using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimerVisual : MonoBehaviour
{
    [SerializeField] List<Sprite> spritesState = new List<Sprite>();
    [SerializeField] float MaxTime;
    public UnityEvent<Sprite> setSpriteEvent;
    public UnityEvent<Color> SetSpriteOpacity;
    [SerializeField] Sprite explode; // >:(


    public void VisualUpdate(float currentTime)
    {
        Color newColor = Color.white;
        if (currentTime > MaxTime)
        {
            newColor.a = 0;
            SetSpriteOpacity?.Invoke(newColor);
            return;
        }

        SetSpriteOpacity?.Invoke(newColor);

        float normalizedTime = currentTime / MaxTime;

        if (normalizedTime < 0)
        {
            setSpriteEvent?.Invoke(explode);
            return;
        }

        normalizedTime *= 10;
        int floooredTime = Mathf.FloorToInt(normalizedTime);
        setSprite(floooredTime);
    }

    void setSprite(int roundedTime)
    {
        if (roundedTime >= spritesState.Count)
        {
            roundedTime = spritesState.Count - 1;
        }


        setSpriteEvent?.Invoke(spritesState[roundedTime]);
    }

}
