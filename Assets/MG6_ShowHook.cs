using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MG6_ShowHook : MonoBehaviour
{
    [SerializeField] private GameObject StartedHookIcon;
    [SerializeField] private List<GameObject> HookIcon = new List<GameObject>();

    public void ShowHookIcon(BobberColor color)
    {
        StartedHookIcon.SetActive(false);
        HookIcon[(int)color].SetActive(true);
    }

}
