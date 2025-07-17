using System.Collections.Generic;
using UnityEngine;

public class ShareComponent : MonoBehaviour
{
    public static ShareComponent instance => getInstance();
    static ShareComponent _instance;
    static ShareComponent getInstance()
    {
        if (_instance == null)
        {
            PreCreate();
        }

        return _instance;
    }

    static void PreCreate()
    {
        GameObject preGameObject = new GameObject();
        _instance = preGameObject.AddComponent<ShareComponent>();
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _instance = this;
    }

    List<MonoBehaviour> storedComponnet = new List<MonoBehaviour>();
    public void AddingComponent(MonoBehaviour compToAdd)
    {
        if (storedComponnet.Contains(compToAdd))
        {
            Destroy(compToAdd.gameObject);
            return;
        }

        storedComponnet.Add(compToAdd);
        compToAdd.transform.parent = this.transform;
    }

    public bool GetComponent<T>(out T searchResult) where T : MonoBehaviour
    {
        searchResult = null;
        foreach (MonoBehaviour comp in storedComponnet)
        {
            searchResult = comp.GetComponent<T>();
            if (searchResult != null)
            {
                return true;
            }
        }


        return false;
    }
}
