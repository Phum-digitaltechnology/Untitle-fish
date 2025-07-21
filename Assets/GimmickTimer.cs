using UnityEngine;

public class GimmickTimer : MonoBehaviour
{
    GimmickManage findGimmick;
    private void Awake()
    {
        findGimmick = FindAnyObjectByType<GimmickManage>();
    }

    public void UpdateTimer(float time)
    {
        if (findGimmick == null)
        {
            return;
        }
        findGimmick.OnTimmerUpdate();
    }
}
