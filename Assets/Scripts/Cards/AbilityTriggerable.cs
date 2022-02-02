using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityTriggerable : MonoBehaviour
{
    protected int mainValueAmount; 

    protected CardSOManager.TargetType targetType;
    protected CardSOManager my_cardSOManager;
    protected CardSOManager target_CardSOManager;

    public void Initialize(CardSOManager.TargetType targetType, int mainValueAmount)
    {
        this.targetType = targetType;
        this.mainValueAmount = mainValueAmount;
        if (!my_cardSOManager) my_cardSOManager = GetComponent<CardSOManager>();
        FindTargetSOManager();
    }

    void FindTargetSOManager()
    {
        var targetField = my_cardSOManager.FindTargetField(targetType);
        target_CardSOManager = my_cardSOManager.FindTargetCard(targetField);

        if (!target_CardSOManager)
        {
            Debug.LogError(gameObject.name + " doesn't have target card");
            return;
        }
    }

    public abstract void UseAbility();
}
