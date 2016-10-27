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
        COUNT
    }

    public enum MagicType
    {
        None = 0,
        Self,
        Light,
        Medium,
        Heavy
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
        const int keyAvailable = 9;
        // + 1 because of the list beginning at 0
        for (int i = 0; i < keyAvailable + 1; i++)
            magicKeySelected.Add(null);
    }

    private void InitMap()
    {
        for (MagicID id = MagicID.NONE + 1; id < MagicID.COUNT; id++)
            mapCachePrefabPaths.Add(id, "Magic/" + id);
    }

    public ASpell CreateSpell(SpellProperty SpellProp, ACharacterController _controller)
    {
        string prefabPath;

        if (!mapCachePrefabPaths.TryGetValue(SpellProp.Id, out prefabPath))
        {
            Debug.LogError("MagicManager.CreateSpell() - could not find prefab path for id [" + SpellProp.Id + "] \"" + SpellProp.Id + "\"");
            return null;
        }
        ASpell spellInstance = Instantiate(ResourceManager.Instance.Load(prefabPath).GetComponent<ASpell>());
        spellInstance.SetController(_controller);
        spellInstance.SetProperty(SpellProp);

        return spellInstance;
    }

    public SpellProperty CreateSpellProperties(MagicID id, MagicType _type, float _power, int _cost, string _description)
    {
        SpellProperty spellProp = new SpellProperty();
        spellProp.Init(id, _type, _power, _cost, _description);
        return spellProp;
    }
}
