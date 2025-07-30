using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GimmickMouseController : MonoBehaviour
{
    [SerializeField] List<Gimmick> IsActive = new List<Gimmick>();
    [SerializeField] UnityEvent onActive, unActive;


    // Update is called once per frame
    void Update()
    {
        foreach (Gimmick g in IsActive)
        {
            if (g.OnActive)
            {
                onActive?.Invoke();
                return;
            }
        }
        unActive?.Invoke();
    }
}
