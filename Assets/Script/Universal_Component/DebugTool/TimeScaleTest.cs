using UnityEngine;

public class TimeScaleTest : MonoBehaviour
{
    [SerializeField] float TimeScale;
    private void Start()
    {
        Time.timeScale = TimeScale;
    }
}
