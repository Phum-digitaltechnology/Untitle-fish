using UnityEngine;
using UnityEngine.Events;
public class CatnipGimmick : Gimmick
{
    bool activeCatNap;
    ScoreSystem scoreSystem;
    [SerializeField] int effectTime;

    [SerializeField] UnityEvent onActiveEffect, unActiveEffect;
    [SerializeField] Transform CatHolder;
    private void Awake()
    {
        scoreSystem = FindAnyObjectByType<ScoreSystem>();
    }
    int currentEffectTimer;
    public override void Active()
    {
        if (activeCatNap || OnActive) return;
        Transform randomedTransform = CatHolder.GetChild(Random.Range(0, CatHolder.childCount));
        CatEatCatNip cat = randomedTransform.GetComponent<CatEatCatNip>();
        cat.gameObject.SetActive(true);
        cat.SetUp(updateState);
        OnActive = true;
        OnActive = true;
    }

    void updateState(bool isActive)
    {
        OnActive = false;
        activeCatNap = isActive;
        if (activeCatNap)
        {
            currentEffectTimer = effectTime;
            scoreSystem.OnCountUpdate += effectTimeUpdate;
            onActiveEffect?.Invoke();
        }
    }

    void effectTimeUpdate(int count)
    {
        currentEffectTimer--;
        if (currentEffectTimer <= 0)
        {
            ResetCD();
            activeCatNap = false;
            scoreSystem.OnCountUpdate -= effectTimeUpdate;
            unActiveEffect?.Invoke();
        }
    }


}
