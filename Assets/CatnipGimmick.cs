using UnityEngine;
using UnityEngine.Events;
public class CatnipGimmick : Gimmick
{
    bool activeCatNap;
    ScoreSystem scoreSystem;
    sceneManager getScene;
    [SerializeField] int effectTime;

    [SerializeField] UnityEvent onActiveEffect, unActiveEffect;
    [SerializeField] Transform CatHolder;
    private void Awake()
    {
        getScene = FindAnyObjectByType<sceneManager>();
        scoreSystem = FindAnyObjectByType<ScoreSystem>();
        getScene.OnLoadingIntoScene += onloadIntoIntermission;
    }
    int currentEffectTimer;

    bool waitActive;
    public override void Active()
    {
        if (activeCatNap || OnActive) return;
        OnActive = true;
        waitActive = true;
    }
    void onloadIntoIntermission(string isIntermission)
    {
        if (isIntermission == "IntermissionMain")
        {
            if (waitActive)
            {
                waitActive = false;
                catAppear();
            }
        }
    }

    void catAppear()
    {
        Transform randomedTransform = CatHolder.GetChild(Random.Range(0, CatHolder.childCount));
        CatEatCatNip cat = randomedTransform.GetComponent<CatEatCatNip>();
        cat.gameObject.SetActive(true);
        cat.SetUp(updateState);
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
