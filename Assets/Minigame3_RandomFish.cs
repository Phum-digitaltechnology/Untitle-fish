using System.Collections.Generic;
using UnityEngine;
public class Minigame3_RandomFish : MonoBehaviour
{
    [System.Serializable]
    private class FishType
    {
        public int UnlockWhen;
        public int chance;
        public GameObject Line;

        public GameObject Bar;
        public Collider2D rawZone;
        public Collider2D mediumZone;
        public Collider2D cookedZone;
        public Collider2D burnedZone;
        public FishVisual rawFishSprite;
        public FishVisual mediumFishSprite;
        public FishVisual cookedFishSprite;
        public FishVisual burnedFishSprite;
    }

    [SerializeField] List<FishType> fishTypes = new List<FishType>();
    [SerializeField] GrillFishMinigame grillFish;
    void Awake()
    {
        ScoreSystem scoreSystem = FindAnyObjectByType<ScoreSystem>();

        if (scoreSystem == null)
        {
            setUpfish(fishTypes[Random.Range(0, fishTypes.Count)]);
            return;
        }

        int currentRoundPlay = scoreSystem.playedMinigameCount;
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
        randFishType.Line.SetActive(true);
        randFishType.Bar.SetActive(true);
        grillFish.rawZone = randFishType.rawZone;
        grillFish.mediumZone = randFishType.mediumZone;
        grillFish.cookedZone = randFishType.cookedZone;
        grillFish.burnedZone = randFishType.burnedZone;
        grillFish.rawFishSprite.FishSprite = randFishType.rawFishSprite.FishSprite;
        grillFish.mediumFishSprite.FishSprite = randFishType.mediumFishSprite.FishSprite;
        grillFish.cookedFishSprite.FishSprite = randFishType.cookedFishSprite.FishSprite;
        grillFish.burnedFishSprite.FishSprite = randFishType.burnedFishSprite.FishSprite;
    }
}
