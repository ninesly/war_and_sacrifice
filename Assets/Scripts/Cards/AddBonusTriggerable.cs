using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBonusTriggerable : AbilityTriggerable
{
    public override void UseAbility()
    {
        target_CardSOManager.AddBonus(mainValueAmount, gameObject);
    }
}
