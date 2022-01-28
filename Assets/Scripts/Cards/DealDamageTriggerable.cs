using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageTriggerable : MonoBehaviour
{
    int dmgAmount;
    CardSOManager.Target target;
    DuelField targetField;

    CardSOManager my_cardSOManager;
    CardSOManager target_CardSOManager;

    private void Start()
    {
        my_cardSOManager = GetComponent<CardSOManager>();
    }
    public void Initialize(int dmgAmount, CardSOManager.Target target)
    {
        this.dmgAmount = dmgAmount;
        this.target = target;
    }

    public void Attack(GameObject whoIsTrying)
    {
        Debug.Log(whoIsTrying.name + " tries to attack");

        if (!my_cardSOManager) my_cardSOManager = GetComponent<CardSOManager>();

        targetField = my_cardSOManager.FindTargetField(target);
        target_CardSOManager = my_cardSOManager.FindTargetCard(targetField);

        if (!target_CardSOManager)
        {
            Debug.LogError(gameObject.name + " doesn't have target card to attack");
            return;
        }

        target_CardSOManager.ReceiveDamage(dmgAmount, gameObject);
    }






}
