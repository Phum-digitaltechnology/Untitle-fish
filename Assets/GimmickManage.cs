using System.Collections.Generic;
using UnityEngine;
public class GimmickManage : MonoBehaviour
{
    ScoreSystem scoreSystem;
    List<Gimmick> gimmickList = new List<Gimmick>();
    List<Gimmick> gimmickActivePool = new List<Gimmick>();

    private void Awake()
    {
        getGimmickList();
        scoreSystem = FindAnyObjectByType<ScoreSystem>();
        scoreSystem.OnCountUpdate += onFinishMinigame;
    }

    public void ResetPool()
    {
        gimmickActivePool = new List<Gimmick>();

        foreach (Gimmick g in gimmickList)
        {
            g.OnReset();
        }
    }

    void getGimmickList()
    {
        foreach (Transform child in this.transform)
        {
            if (child.TryGetComponent<Gimmick>(out Gimmick getGimmick))
            {
                gimmickList.Add(getGimmick);
            }
        }
    }

    void onFinishMinigame(int currentCount)
    {

        foreach (Gimmick gimmick in gimmickList)
        {
            if (gimmick.UnLockWhen <= currentCount)
            {
                if (gimmickActivePool.Contains(gimmick)) continue;
                gimmickActivePool.Add(gimmick);
            }
        }
    }

    public void OnTimmerUpdate()
    {
        foreach (Gimmick isActive in gimmickActivePool)
        {
            isActive.AddCD();
            if (isActive.IsActive)
            {
                isActive.ResetCD();
                isActive.Active();
            }
        }
    }
}
