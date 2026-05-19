using UnityEngine;

public class SmartFPSSetup : MonoBehaviour
{
    [Header("FPS Settings")]
    [SerializeField] int webglFPS = 60;
    [SerializeField] int mobileFPS = 30;
    [SerializeField] int desktopFPS = -1; // -1 = unlimited

    void Awake()
    {
        SetupFPS();
    }

    void SetupFPS()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        // WebGL runs inside browser → disable Unity VSync
        QualitySettings.vSyncCount = 0;

        // Lock to stable 60 FPS
        Application.targetFrameRate = webglFPS;

        Debug.Log($"[SmartFPS] WebGL detected → VSync OFF | FPS = {webglFPS}");

#elif UNITY_ANDROID || UNITY_IOS

        // Mobile → save battery & heat
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = mobileFPS;

        Debug.Log($"[SmartFPS] Mobile detected → FPS = {mobileFPS}");

#else

        // Desktop / Editor
        QualitySettings.vSyncCount = 1;   // allow monitor sync
        Application.targetFrameRate = desktopFPS;

        Debug.Log($"[SmartFPS] Desktop detected → VSync ON | FPS = {desktopFPS}");

#endif
    }
}