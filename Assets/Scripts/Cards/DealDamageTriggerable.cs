using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageTriggerable : AbilityTriggerable
{
    CardSOManager.DestroyType destroyType;

    public void SetDestroyType(CardSOManager.DestroyType destroyType)
    {
        this.destroyType = destroyType;
    }

    public override void UseAbility()
    {
        target_CardSOManager.ReceiveDamage(mainValueAmount, gameObject, destroyType);
    }
}
