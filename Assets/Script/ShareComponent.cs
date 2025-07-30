using MoreMountains.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShareComponent : MMSingleton<ShareComponent>
{
    List<MonoBehaviour> storedComponnet = new List<MonoBehaviour>();
    List<Type> addedType = new List<Type>();
    public void AddingComponent<T>(T compToAdd) where T : MonoBehaviour
    {
        if (addedType.Contains(typeof(T)))
        {
            Destroy(compToAdd);
            return;
        }
        storedComponnet.Add(compToAdd);
        addedType.Add(typeof(T));

        DontDestroyOnLoad(compToAdd);
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