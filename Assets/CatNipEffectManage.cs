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
    [SerializeField] bool isActive = false;

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
                c.transform.Rotate(c.transform.rotation.x, c.transform.rotation.y, 180);
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
                c.transform.Rotate(c.transform.rotation.x, c.transform.rotation.y, 0);

            }

            foreach (InvertAble c in allCanvas)
            {
                c.IsInvert(false);

            }
        }
    }
}
