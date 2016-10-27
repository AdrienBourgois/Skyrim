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

    public GameObject Load(string _pathPrefab)
    {
        GameObject prefab;

        if (!mapCachePrefab.TryGetValue(_pathPrefab, out prefab))
        {
            prefab = Resources.Load<GameObject>(_pathPrefab);
            if (prefab == null)
            {
                Debug.LogError("ResourceManager.Load() couldn't load prefab with path \"" + _pathPrefab + "\"");
                return null;
            }
            mapCachePrefab.Add(_pathPrefab, prefab);
        }

        return prefab;
    }

    public T Load<T>(string _pathPrefab)
    {
        GameObject prefab;

        if (!mapCachePrefab.TryGetValue(_pathPrefab, out prefab))
        {
            prefab = Resources.Load<GameObject>(_pathPrefab);
            if (prefab == null)
            {
                Debug.LogError("ResourceManager.Load() couldn't load prefab with path \"" + _pathPrefab + "\"");
                return default(T);
            }
            mapCachePrefab.Add(_pathPrefab, prefab);
        }
        
        T prefabTemplate = prefab.GetComponent<T>();

        if (prefabTemplate == null)
        {
            Type typeOfT = typeof(T);
            Debug.LogError("ResourceManager.Load() couldn't get component of type \"" + typeOfT + "\" with path \"" + _pathPrefab + "\"");
            return default(T);
        }

        return prefabTemplate;
    }
}
