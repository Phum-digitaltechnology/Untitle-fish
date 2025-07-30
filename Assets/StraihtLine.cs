using UnityEngine;

public class StraihtLine : MonoBehaviour
{
    [SerializeField] Transform StartPos;
    [SerializeField] Transform EndPos;
    [SerializeField] LineRenderer LineRenderer;

    void Update()
    {
        LineRenderer.SetPosition(0, StartPos.transform.position);
        LineRenderer.SetPosition(1, EndPos.transform.position);
    }
}
