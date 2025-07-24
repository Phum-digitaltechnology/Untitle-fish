using UnityEngine;
using System.Collections.Generic;
public class RandomFish : MonoBehaviour
{
    [System.Serializable    ]
    private class FishType
    {
        [SerializeField] Sprite fishSprite;
        public Sprite FishSprite => fishSprite;

        [SerializeField] Sprite fearFishSprite;
        public Sprite FearFishSprite => fearFishSprite;
        [SerializeField] float moveSpeed;
        public float MoveSpeed => moveSpeed;
        public int chance = 50;
        public int UnlockWhen = 0;
    }

    [SerializeField] List<FishType> fishTypes = new List<FishType>();
    [SerializeField] List<SpriteRenderer> fishNormalRender = new List<SpriteRenderer>();   
        [SerializeField] List<SpriteRenderer> fishFearRender = new List<SpriteRenderer>();   

[SerializeField] FishRun fishRun;
    void Awake()
    {
        int currentRoundPlay = FindAnyObjectByType<ScoreSystem>().playedMinigameCount;
        List<FishType> fishPool = new List<FishType>();

        int maxCount = 0;

        foreach (FishType fish in fishTypes)
        {
            if (fish.UnlockWhen <= currentRoundPlay)
            {
                maxCount += fish.chance;
                fishPool.Add(fish);
            }
        }
        int currentCount = 0;
        int randValue = Random.Range(0, maxCount);

        for (int i = 0; i < fishPool.Count; i++)
        {
            currentCount += fishPool[i].chance;
            if (currentCount > randValue)
            {
                setUpfish(fishPool[i]);
                break;
            }
        }
    }

    void setUpfish(FishType randFishType)
    {
        foreach (SpriteRenderer fishRenderer in fishNormalRender)
        {
            fishRenderer.sprite = randFishType.FishSprite;
        }

        foreach (SpriteRenderer fishRenderer in fishFearRender)
        {
            fishRenderer.sprite = randFishType.FearFishSprite;
        }   
        fishRun.changeMoveSpeed(randFishType.MoveSpeed);
    }
}
