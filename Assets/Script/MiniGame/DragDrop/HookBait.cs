using UnityEngine;
using UnityEngine.Events;

public class HookBait : MonoBehaviour
{
    [SerializeField] private UnityEvent OnApplyBait;
    //[SerializeField] private GameObject confetti;
    private bool haveWorm = false;
    bool isWormOnHook;
    Collider2D col;
    void OnTriggerStay2D(Collider2D col)
    {
        this.col = col;
        isWormOnHook = col.gameObject.tag == "Worm";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isWormOnHook = false;
        this.col = null;
    }

    private void Update()
    {
        if (isWormOnHook)
        {
            if (Input.GetMouseButtonUp(0) && !haveWorm)
            {
                Destroy(col.gameObject.GetComponent<Rigidbody2D>());
                OnApplyBait?.Invoke();
                //Instantiate(confetti, new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y, col.gameObject.transform.position.z), Quaternion.identity);
                haveWorm = true;
            }
        }
    }
}