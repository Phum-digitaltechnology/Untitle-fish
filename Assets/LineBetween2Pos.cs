using UnityEngine;

public class LineBetween2Pos : MonoBehaviour
{
    [SerializeField] Transform FirstPosition;
    [SerializeField] Transform SecondPosition;

    LineRenderer lineRenderer;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }


    private void Update()
    {
        if (lineRenderer.positionCount != 2) return;
        lineRenderer.SetPosition(0, FirstPosition.position);
        lineRenderer.SetPosition(1, SecondPosition.position);
    }


    public void CancelLine()
    {
        lineRenderer.positionCount = 0;
    }
}
