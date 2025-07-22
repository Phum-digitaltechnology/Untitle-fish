using UnityEngine;
using UnityEngine.Events;
public class CatnipGimmick : Gimmick
{
    bool boolOnactive, activeCatNap;
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
        if (activeCatNap || boolOnactive) return;
        Debug.Log("Cat Active");
        Transform randomedTransform = CatHolder.GetChild(Random.Range(0, CatHolder.childCount));
        CatEatCatNip cat = randomedTransform.GetComponent<CatEatCatNip>();
        cat.gameObject.SetActive(true);
        cat.SetUp(updateState);
        boolOnactive = true;
    }

    void updateState(bool isActive)
    {
        boolOnactive = false;
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
