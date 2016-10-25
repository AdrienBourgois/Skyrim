using UnityEngine;
using System.Collections.Generic;

public class SpellInventory
{
    private List<Spell> spellList = new List<Spell>();
    public List<Spell> SpellList
    {
       get { return spellList; }
    }

    public void PlayerBasicSpellInit()
    {
        spellList.Add(CreateSpell("FireBall", -50f, 20, "Cast a Powerfull Fireball which can ignite the opponent"));
        spellList.Add(CreateSpell("Heal", 30f, 10, "Heal Yourself with a bright wave of light"));
        spellList.Add(CreateSpell("Invisibility", 0, 50, "Cast the power of the dark night to become Invisible ... But your weapon are still visible ..."));
    }

    Spell CreateSpell(string name, float value, int cost, string description)
    {
        Spell spell = new Spell();
        spell.Init(name, value, cost, description);

        return spell;
    }
}
