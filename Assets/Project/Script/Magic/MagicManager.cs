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
        Buff = 3,
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
    
    private List<SpellProperty> magicKeySelected = new List<SpellProperty>();
    public List<SpellProperty> MagicKeySelected
    { get { return magicKeySelected; } }

    private void Start()
    {
        InitKeyList();
        InitMap();
    }

    private void InitKeyList()
    {
        int keyAvailable = 9;
        // + 1 because of the list beginning at 0
        for (int i = 0; i < keyAvailable + 1; i++)
            magicKeySelected.Add(null);
    }

    private void InitMap()
    {
        for (MagicID id = MagicID.Heal; id < MagicID.COUNT; id++)
            mapCachePrefabPaths.Add(id, "Magic/" + id.ToString());
    }

    public ASpell CreateSpell(SpellProperty magic)
    {
        string prefabPath;

        if (!mapCachePrefabPaths.TryGetValue(magic.ID, out prefabPath))
        {
            Debug.LogError("MagicManager.CreateSpell() - could not find prefab path for id [" + (int)magicID + "] \"" + magicID.ToString() + "\"");
            return null;
        }

        ASpell spellInstance = Instantiate(ResourceManager.Instance.Load(prefabPath));
        //spellInstance.setController()

        return spellInstance;
    }

    public SpellProperty CreateMagic<T>(string name, float power, int cost, float lifeTime, string description, MagicManager.MagicID id, MagicManager.MagicType type) where T : SpellProperty, new()
    {
        T magic = new T();
        magic.Init(name, power, cost, lifeTime, description, id, type);
        return magic;
    }
}
