using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Abilities that deal damages
 * 
 */

[CreateAssetMenu(menuName = "Abilities/AttackAbility")]
public class AttackAbility : Ability
{
    [SerializeField] int damage = 1; // basic dmg for ability, it can be later altered by owner of the ability
    [SerializeField] CardSOManager.DestroyType destroyType; // element that needs to be updated according to a game mechanic, important for TurnManager script

    DealDamageTriggerable dealDmg;


    public override void Initialize(GameObject obj, int finalDamage)
    {
        dealDmg = obj.GetComponent<DealDamageTriggerable>();
        if (!dealDmg)
        {
            Debug.LogError("There is no " + dealDmg);
            return;
        }
        dealDmg.Initialize(targetType, finalDamage);
        dealDmg.SetDestroyType(destroyType);
    }

    public override void TriggerAbility(GameObject whoIsTrying)
    {
        Debug.Log(whoIsTrying + " uses " + name);
        dealDmg.UseAbility();
    }

    public override int GetValue()
    {
        return damage;
    }

    public override TurnManager.DuelSubphases GetSubphase()
    {
        return subphase;
    }
}
