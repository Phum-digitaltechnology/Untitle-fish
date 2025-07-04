using UnityEngine;

public class MG_7LoseSwitcher : MonoBehaviour
{

    bool IsClickedSpaceBar;

    [SerializeField] GameObject missClickGameObject;
    [SerializeField] GameObject FishObject;
    [SerializeField] GameObject HookObject;
    [SerializeField] FishRun moveFish;
    public void IsClickSpaceBar()
    {
        IsClickedSpaceBar = true;
    }

    public void PlayLose()
    {
        if (IsClickedSpaceBar)
        {
            missClickGameObject.SetActive(true);
            HookObject.SetActive(false);
        }
        moveFish.SetFishToStop();
        moveFish.transform.parent = null;
        moveFish.gameObject.SetActive(true);
    }
}
