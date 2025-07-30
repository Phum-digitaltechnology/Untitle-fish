using System.Collections;
using UnityEngine;
public class FreezePosition : MonoBehaviour
{
    [SerializeField] float freezeDelay;
    private void Start()
    {
        StartCoroutine(freeze());
    }


    IEnumerator freeze()
    {
        yield return new WaitForSeconds(freezeDelay);
        this.transform.parent = null;
    }
}
