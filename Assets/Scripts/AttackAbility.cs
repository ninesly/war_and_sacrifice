using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/AttackAbility")]
public class AttackAbility : Ability
{
    [SerializeField] int damage = 1;
    [SerializeField] DealDamageTriggerable.Target target;

    DealDamageTriggerable dealDmg;

    public override void Initialize(GameObject obj)
    {
        dealDmg = obj.GetComponent<DealDamageTriggerable>();
        if (!dealDmg)
        {
            Debug.LogError("There is no dealDmg");
            return;
        }
        dealDmg.Initialize(damage, target);
    }

    public override void TriggerAbility(GameObject whoIsTrying)
    {
        dealDmg.Attack(whoIsTrying);
    }
}