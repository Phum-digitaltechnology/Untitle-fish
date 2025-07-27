using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CatnipGimmick : Gimmick
{
    bool activeCatNap;
    ScoreSystem scoreSystem;
    sceneManager getScene;
    [SerializeField] int effectTime;

    [SerializeField] float Delay = 0.25f;
    [SerializeField] UnityEvent waitRanOutEffect;
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

    Action waitAction;

    public override void Active()
    {
        if (activeCatNap || OnActive) return;
        OnActive = true;
        waitActive = true;
        waitAction = catAppear;
        Debug.Log($"{waitAction} Im Waiting to be played :(");
    }
    void onloadIntoIntermission(string isIntermission)
    {
        if (isIntermission == "IntermissionMain")
        {
            if (waitActive)
            {
                waitActive = false;
                StartCoroutine(delayActive());
            }
        }
    }


    IEnumerator delayActive()
    {
        yield return new WaitForSeconds(Delay);
        waitAction?.Invoke();
    }

    void catAppear()
    {
        Transform randomedTransform = CatHolder.GetChild(UnityEngine.Random.Range(0, CatHolder.childCount));
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
            waitActive = true;
            waitAction = invokeWaitRanoutEffect;
        }
    }

    void invokeWaitRanoutEffect()
    {
        waitRanOutEffect?.Invoke();
    }


    public void effectRanOut()
    {
        ResetCD();
        activeCatNap = false;
        scoreSystem.OnCountUpdate -= effectTimeUpdate;
        unActiveEffect?.Invoke();
    }


}
