using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageTriggerable : MonoBehaviour
{
    int dmgAmount;
    CardSOManager.TargetType targetType;
    CardSOManager.DestroyType destroyType;
    DuelField targetField;

    CardSOManager my_cardSOManager;
    CardSOManager target_CardSOManager;

    private void Start()
    {
        my_cardSOManager = GetComponent<CardSOManager>();
    }
    public void Initialize(CardSOManager.TargetType targetType, int dmgAmount, CardSOManager.DestroyType destroyType)
    {
        this.targetType = targetType;
        this.dmgAmount = dmgAmount;
        this.destroyType = destroyType;
    }

    public void Attack(GameObject whoIsTrying)
    {
        Debug.Log(whoIsTrying.name + " tries to attack");

        if (!my_cardSOManager) my_cardSOManager = GetComponent<CardSOManager>();

        targetField = my_cardSOManager.FindTargetField(targetType);
        target_CardSOManager = my_cardSOManager.FindTargetCard(targetField);

        if (!target_CardSOManager)
        {
            Debug.LogError(gameObject.name + " doesn't have target card to attack");
            return;
        }

        target_CardSOManager.ReceiveDamage(dmgAmount, gameObject, destroyType);
    }






}
