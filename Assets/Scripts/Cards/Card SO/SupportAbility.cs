using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Abilities that deal damages
 * 
 */

[CreateAssetMenu(menuName = "Abilities/SupportAbility")]
public class SupportAbility : Ability
{
    
    [SerializeField] int bonus = 1; // basic dmg for ability, it can be later altered by owner of the ability
    
    AddBonusTriggerable addBonus;

    
    public override void Initialize(GameObject obj, int finalBonus)
    {
        addBonus = obj.GetComponent<AddBonusTriggerable>();
        if (!addBonus)
        {
            Debug.LogError("There is no " + addBonus);
            return;
        }
        addBonus.Initialize(targetType, finalBonus);
    }

    public override void TriggerAbility(GameObject whoIsTrying)
    {
        Debug.Log(whoIsTrying + " uses " + name);
        addBonus.UseAbility();
    }

    public override int GetValue()
    {
        return bonus;
    }

    public override TurnManager.DuelSubphases GetSubphase()     
    {
        return subphase;
    }
}
