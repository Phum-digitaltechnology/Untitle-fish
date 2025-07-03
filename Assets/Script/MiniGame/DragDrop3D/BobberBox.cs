using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BobberBox : MonoBehaviour
{

    [SerializeField] private List<GameObject> Bobber3D = new List<GameObject>();

    public void Setup()
    {
        foreach (GameObject b in Bobber3D)
        {
            b.GetComponent<MG6_DragDrop3D>().SetUp();
        }
    }

}
