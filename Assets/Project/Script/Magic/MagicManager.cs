using UnityEngine;
using System.Collections.Generic;

public class MagicManager : MonoBehaviour
{
    static private MagicManager instance;
    static public MagicManager Instance
    {
        get { return instance ?? (instance = FindObjectOfType<MagicManager>()); }
    }

    public enum MagicID
    {
        NONE = 0,
        Heal,
        Fireball,
        Invisibility,
        COUNT,
    }

    public enum MagicType
    {
        None = 0,
        Self,
        Light,
        Medium,
        Heavy,
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
        for (MagicID id = MagicID.NONE + 1; id < MagicID.COUNT; id++)
            mapCachePrefabPaths.Add(id, "Magic/" + id.ToString());
    }

    public ASpell CreateSpell(SpellProperty magic, ACharacterController controller)
    {
        string prefabPath;

        if (!mapCachePrefabPaths.TryGetValue(magic.ID, out prefabPath))
        {
            Debug.LogError("MagicManager.CreateSpell() - could not find prefab path for id [" + magic.ID + "] \"" + magic.ID.ToString() + "\"");
            return null;
        }
        Debug.Log(prefabPath);
        ASpell spellInstance = Instantiate(ResourceManager.Instance.Load(prefabPath).GetComponent<ASpell>());
        spellInstance.SetController(controller);

        return spellInstance;
    }

    public SpellProperty CreateMagic(MagicManager.MagicID id, MagicManager.MagicType type, float power, int cost, string description)
    {
        SpellProperty magic = new SpellProperty();
        magic.Init(id, type, power, cost, description);
        return magic;
    }
}
