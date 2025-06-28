using UnityEngine;
using UnityEngine.Events;

public class HookBait : MonoBehaviour
{
    [SerializeField] private UnityEvent OnApplyBait;
    [SerializeField] private GameObject confetti;
    private bool haveWorm = false;

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Worm")
        {
            //Debug.Log("Trigger Stay");
            if (Input.GetMouseButtonUp(0) && !haveWorm)
            {
                Destroy(col.gameObject.GetComponent<Rigidbody2D>());
                OnApplyBait?.Invoke();
                Instantiate(confetti, new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y, col.gameObject.transform.position.z), Quaternion.identity);
                haveWorm = true;
            }
        }
    }
}