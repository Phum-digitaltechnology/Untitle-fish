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
        Debug.Log("[Gimmick] onFinish Minigame");

        List<Gimmick> removeList = new List<Gimmick>();
        foreach (Gimmick gimmick in gimmickList)
        {
            if (gimmick.UnLockWhen >= currentCount)
            {
                gimmickActivePool.Add(gimmick);
                removeList.Add(gimmick);
            }
        }

        foreach (Gimmick removed in removeList)
        {
            removed.ResetCD();
            gimmickList.Remove(removed);
        }
    }

    public void OnTimmerUpdate()
    {
        Debug.Log("[Gimmick] Timer Update");
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
