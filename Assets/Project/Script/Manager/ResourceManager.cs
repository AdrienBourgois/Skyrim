using UnityEngine;
using System.Collections.Generic;
using System;

public class ResourceManager : MonoBehaviour
{
    static private ResourceManager instance;
    static public ResourceManager Instance
    {
        get { return instance ?? (instance = FindObjectOfType<ResourceManager>()); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private Dictionary<string, GameObject> mapCachePrefab = new Dictionary<string, GameObject>();

    public T Load<T>(string pathPrefab)
    {
        GameObject prefab;

        if (!mapCachePrefab.TryGetValue(pathPrefab, out prefab))
        {
            prefab = Resources.Load<GameObject>(pathPrefab);
            if (prefab == null)
            {
                Debug.LogError("ResourceManager.Load() couldn't load prefab with path \"" + pathPrefab + "\"");
                return default(T);
            }
            mapCachePrefab.Add(pathPrefab, prefab);
        }
        
        T prefabTemplate = prefab.GetComponent<T>();

        if (prefabTemplate == null)
        {
            Type typeOfT = typeof(T);
            Debug.LogError("ResourceManager.Load() couldn't get component of type \"" + typeOfT + "\" with path \"" + pathPrefab + "\"");
            return default(T);
        }

        return prefabTemplate;
    }
}
