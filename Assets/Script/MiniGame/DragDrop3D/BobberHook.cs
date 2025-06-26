using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BobberHook : MonoBehaviour
{
    [SerializeField] private List<GameObject> BobberUI = new List<GameObject> ();
    private BobberColor correctBobber;
    [SerializeField] private UnityEvent onCorrectBobber;
    [SerializeField] private GameObject confetti;

    public void Setup()
    {
        correctBobber = (BobberColor)Random.Range(0, 5);
        BobberUI[(int)correctBobber].gameObject.SetActive(true);
        
    }

    public void CheckBobber(BobberColor bobberColor)
    {
        if (bobberColor == correctBobber)
        {
            onCorrectBobber?.Invoke();
            Instantiate(confetti, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        }
    }
}
