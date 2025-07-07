using UnityEngine;

public class BounceAnim : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int BouncePow;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        Drop();
    }

    public void Drop()
    {
        Spin();
        rb.gravityScale = 1;
        rb.AddForce(Vector2.up * BouncePow + (Vector2.right * (Random.Range(-10, 10) / 10)));
    }
    public void Spin()
    {
        Quaternion target = Quaternion.AngleAxis(90f, Vector3.down);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, Time.deltaTime);
    }
}
