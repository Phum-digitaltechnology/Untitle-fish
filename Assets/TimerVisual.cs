using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimerVisual : MonoBehaviour
{
    [SerializeField] List<Sprite> spritesState = new List<Sprite>();
    public UnityEvent<Sprite> setSpriteEvent;
    public UnityEvent<Color> SetSpriteOpacity;

    public void VisualUpdate(float currentTime)
    {
        int floorTime = Mathf.FloorToInt(currentTime);

        if (floorTime > spritesState.Count)
        {
            Color myColor = Color.white;

            myColor.a = 0f; //
            SetSpriteOpacity?.Invoke(myColor);
        }
        else
        {
            Color myColor = Color.white;
            SetSpriteOpacity?.Invoke(myColor);
            if (floorTime == 0)
            {
                setSprite(0);
                return;
            }
            setSprite(floorTime - 1);
        }
    }

    void setSprite(int roundedTime)
    {
        setSpriteEvent?.Invoke(spritesState[roundedTime]);
    }

}
