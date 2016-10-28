using UnityEngine;
using System.Collections.Generic;

public class MagicManager : MonoBehaviour
{
    static private MagicManager instance;
    static public MagicManager Instance
    {
        get { return instance ?? (instance = FindObjectOfType<MagicManager>()); }
    }

    public enum MagicId
    {
        None = 0,
        Heal,
        Fireball,
        Invisibility,
        Count
    }

    public enum MagicType
    {
        None = 0,
        Self,
        Light,
        Medium,
        [Useless]
        Heavy
    }

    private Dictionary<MagicId, string> mapCachePrefabPaths = new Dictionary<MagicId, string>();
    
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
        for (MagicId id = MagicId.None + 1; id < MagicId.Count; id++)
            mapCachePrefabPaths.Add(id, "Magic/" + id);
    }

    public ASpell CreateSpell(SpellProperty _spellProp, ACharacterController _controller)
    {
        string prefabPath;

        if (!mapCachePrefabPaths.TryGetValue(_spellProp.Id, out prefabPath))
        {
            Debug.LogError("MagicManager.CreateSpell() - could not find prefab path for id [" + _spellProp.Id + "] \"" + _spellProp.Id + "\"");
            return null;
        }
        ASpell spellInstance = Instantiate(ResourceManager.Instance.Load(prefabPath).GetComponent<ASpell>());
        spellInstance.SetController(_controller);
        spellInstance.SetProperty(_spellProp);

        return spellInstance;
    }

    public SpellProperty CreateSpellProperties(MagicId _id, MagicType _type, float _power, int _cost, string _description)
    {
        SpellProperty spellProp = new SpellProperty();
        spellProp.Init(_id, _type, _power, _cost, _description);
        return spellProp;
    }
}
