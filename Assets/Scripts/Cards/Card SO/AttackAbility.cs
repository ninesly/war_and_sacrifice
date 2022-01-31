using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/AttackAbility")]
public class AttackAbility : Ability
{
    [SerializeField] int damage = 1;
    [SerializeField] CardSOManager.Target target;

    DealDamageTriggerable dealDmg;


    public override void Initialize(GameObject obj, int cardDamage)
    {
        dealDmg = obj.GetComponent<DealDamageTriggerable>();
        if (!dealDmg)
        {
            Debug.LogError("There is no dealDmg");
            return;
        }
        dealDmg.Initialize(target, cardDamage);
    }

    public override void TriggerAbility(GameObject whoIsTrying)
    {
        dealDmg.Attack(whoIsTrying);
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
