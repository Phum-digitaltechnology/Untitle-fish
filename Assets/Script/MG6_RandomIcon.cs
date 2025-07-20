using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MG6_RandomIcon : MonoBehaviour
{

    [SerializeField] private List<GameObject> Icon = new List<GameObject>();

    public void RandomActivateIcon()
    {
        int randomNum = Random.Range(0, Icon.Count);
        Icon[randomNum].SetActive(true);
    }

}
