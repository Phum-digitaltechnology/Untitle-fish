using System.Collections.Generic;
using UnityEngine;

public class CatNipEffectManage : MonoBehaviour
{
    private void Awake()
    {
        FindAnyObjectByType<sceneManager>().OnLoadingIntoScene += OnLoadNewScene;
    }

    List<Camera> allMyComponents = new List<Camera>();
    List<InvertAble> allCanvas = new List<InvertAble>();
    bool isActive = false;

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        OnLoadNewScene("Something");
    }


    void OnLoadNewScene(string scene)
    {
        Debug.Log($"Loading into Scene Name {scene}");
        allMyComponents = new List<Camera>(FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        allCanvas = new List<InvertAble>(FindObjectsByType<InvertAble>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        activeCatNipEffect();
    }

    void activeCatNipEffect()
    {
        if (isActive)
        {
            foreach (Camera c in allMyComponents)
            {
                if (c.TryGetComponent<invertRotation>(out invertRotation invertCam))
                {
                    invertCam.IsInvert(true);
                }
                else
                {
                    invertRotation invert = c.gameObject.AddComponent<invertRotation>();
                    invert.IsInvert(true);
                }
            }

            foreach (InvertAble c in allCanvas)
            {
                c.IsInvert(true);
            }
        }
        else
        {
            foreach (Camera c in allMyComponents)
            {
                if (c.TryGetComponent<invertRotation>(out invertRotation invertCam))
                {
                    invertCam.IsInvert(false);
                }
            }

            foreach (InvertAble c in allCanvas)
            {
                c.IsInvert(false);
            }
        }
    }
}
