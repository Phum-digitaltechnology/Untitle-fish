using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerMMF : MonoBehaviour
{
    [SerializeField] List<MMF_Player> player = new List<MMF_Player>();

    public void RandomPlay()
    {
        MMF_Player ToPlay = player[Random.Range(0, player.Count)];
        ToPlay.PlayFeedbacks();
    }
}
