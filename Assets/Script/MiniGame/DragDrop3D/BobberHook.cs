using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BobberHook : MonoBehaviour
{
    [SerializeField] private List<GameObject> BobberUI = new List<GameObject> ();
    private BobberColor correctBobber;
    [SerializeField] private UnityEvent onCorrectBobber;
    [SerializeField] private UnityEvent onIncorrectBobber;
    [SerializeField] private GameObject winningStarPrefab;
    [SerializeField] private GameObject winningStarLocation;
    [SerializeField] private GameObject confettiPrefab;
    [SerializeField] private GameObject confettiLocation;
    [SerializeField] private GameObject HookIcon;

    public void Setup()
    {
        correctBobber = (BobberColor)Random.Range(0, 5);
        BobberUI[(int)correctBobber].gameObject.SetActive(true);
        
    }

    public void CheckBobber(BobberColor bobberColor)
    {
        if (bobberColor == correctBobber)
        {
            AudioManager.Instance.PlaySFX("YAY");
            BobberUI[(int)correctBobber].gameObject.SetActive(false);
            HookIcon.GetComponent<MG6_ShowHook>().ShowHookIcon(correctBobber);
            onCorrectBobber?.Invoke();
            Instantiate(winningStarPrefab, new Vector3(this.winningStarLocation.transform.position.x, this.winningStarLocation.transform.position.y, this.winningStarLocation.transform.position.z), Quaternion.identity);
            Instantiate(confettiPrefab, new Vector3(this.confettiLocation.transform.position.x, this.confettiLocation.transform.position.y, this.confettiLocation.transform.position.z), Quaternion.identity);
        }
        else if (bobberColor != correctBobber)
        {
            BobberUI[(int)correctBobber].gameObject.SetActive(false);
            HookIcon.GetComponent<MG6_ShowHook>().ShowHookIcon(bobberColor);
            onIncorrectBobber?.Invoke();
            AudioManager.Instance.PlaySFX("SadWomp");
        }
    }
}
