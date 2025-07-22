using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FishGimmick : Gimmick
{
    [SerializeField] Canvas Canvas;

    [SerializeField] List<RectTransform> fishPool = new List<RectTransform>();

    [SerializeField] UnityEvent _onActive, _onUnActive;

    private void Start()
    {
        OnUnActive();
    }

    public override void Active()
    {
        RectTransform fish2Spawn = fishPool[Random.Range(0, fishPool.Count)];
        RectTransform randomFish = Instantiate(fish2Spawn, Canvas.transform);

        randomFish.GetComponent<FishVisualObscure>().SetUP();
        MoveImageToRandomPosition(Canvas, randomFish);
        randomFish.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
    void MoveImageToRandomPosition(Canvas canvas, RectTransform imageRect)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        float imageWidth = imageRect.rect.width;
        float imageHeight = imageRect.rect.height;

        float minX = -canvasWidth / 2 + imageWidth / 2;
        float maxX = canvasWidth / 2 - imageWidth / 2;
        float minY = -canvasHeight / 2 + imageHeight / 2;
        float maxY = canvasHeight / 2 - imageHeight / 2;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        imageRect.anchoredPosition = new Vector2(randomX, randomY);
    }
    bool state = false;
    private void Update()
    {
        bool getState = this.transform.childCount > 0;

        if (getState != state)
        {
            state = getState;
            if (state == true)
            {
                OnActive();
            }
            else
            {
                OnUnActive();
            }
        }
    }

    void OnUnActive()
    {
        _onUnActive?.Invoke();
    }

    void OnActive()
    {
        _onActive?.Invoke();
    }
}
