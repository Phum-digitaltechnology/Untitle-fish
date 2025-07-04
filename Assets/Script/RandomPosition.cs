using UnityEngine;
public class RandomPosition : MonoBehaviour
{
    [SerializeField] Transform minTransform;
    [SerializeField] Transform maxTransform;

    [SerializeField] BoxCollider biggestObject;
    [SerializeField] BoxCollider smallObject;


    [ContextMenu("Test Random")]
    public void SetUp()
    {
        randBiggestObj(biggestObject.transform);
        randSmallObj();
    }
    void randBiggestObj(Transform changePos)
    {
        float randX = Random.Range(minTransform.position.x, maxTransform.position.x);
        Vector3 finalPos = new Vector3(randX, changePos.position.y, changePos.position.z);
        changePos.transform.position = finalPos;
    }

    void randSmallObj()
    {
        float randX = Random.Range(minTransform.position.x, maxTransform.position.x);
        Vector3 finalPos = new Vector3(randX, smallObject.transform.position.y, smallObject.transform.position.z);
        smallObject.transform.transform.position = finalPos;

        while (AreCollidersIntersecting(biggestObject, smallObject))
        {
            randX = Random.Range(minTransform.position.x, maxTransform.position.x);
            finalPos = new Vector3(randX, smallObject.transform.position.y, smallObject.transform.position.z);
            smallObject.transform.transform.position = finalPos;
        }
    }

    static bool AreCollidersIntersecting(Collider a, Collider b)
    {
        return Physics.ComputePenetration(
            a, a.transform.position, a.transform.rotation,
            b, b.transform.position, b.transform.rotation,
            out _, out _);
    }


}
