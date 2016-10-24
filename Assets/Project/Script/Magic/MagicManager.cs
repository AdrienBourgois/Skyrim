using UnityEngine;
using System.Collections.Generic;
using System;

public class MagicManager : MonoBehaviour
{
    static private MagicManager instance;
    static public MagicManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MagicManager>();
            return instance;
        }
    }

    public enum MagicID
    {
        NONE = 0,
        Heal = 1,
        Fire = 2,
        COUNT,
    }

    public enum MagicType
    {
        None = 0,
        Self = 1,
        Light = 2,
        Medium = 3,
        Heavy = 4,
    }

    private Dictionary<MagicID, string> mapCachePrefabPaths = new Dictionary<MagicID, string>();
    
    private void Start()
    {
        InitMap();
    }

    private void InitMap()
    {
        for (MagicID id = MagicID.Heal; id < MagicID.COUNT; id++)
            mapCachePrefabPaths.Add(id, "Magic/" + id.ToString());
    }

    public AMagic CreateSpell(MagicID magicID)
    {
        string prefabPath;

        if (!mapCachePrefabPaths.TryGetValue(magicID, out prefabPath))
        {
            Debug.LogError("MagicManager.CreateSpell() - could not find prefab path for id [" + (int)magicID + "] \"" + magicID.ToString() + "\"");
            return null;
        }

        return Instantiate(ResourceManager.Instance.Load<AMagic>(prefabPath));
    }
}
