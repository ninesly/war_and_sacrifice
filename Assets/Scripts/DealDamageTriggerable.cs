using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardManager))]
public class DealDamageTriggerable : MonoBehaviour
{
    public enum Target { EnemyFighter, EnemyHand }

    int damage;
    Target target;
    DuelField targetField;
    GameSession.Users userOfCard;

    CardManager targetCard;

    public void Initialize(int dmgAmount, Target target)
    {
        damage = dmgAmount;
        this.target = target;
        userOfCard = GetComponent<CardManager>().GetUserOfCard();
        FindTargetField();
    }

    public void Attack(GameObject whoIsTrying)
    {
        Debug.LogError(whoIsTrying.name + " tries to attack");

        FindTargetCard();
        if (!targetCard)
        {
            Debug.LogError(gameObject.name + " can't find target card");
            return;
        }
        targetCard.ReceiveDamage(damage);
    }

    void FindTargetCard()
    {
        if (!targetField)
        {
            Debug.LogError(gameObject.name + " can't find target field");
            return;
        }
        targetCard = targetField.GetComponentInChildren<CardManager>();
        if (!targetCard)
        {
            Debug.LogError(gameObject.name + " can't find target card on " + targetField.gameObject.name);
            return;
        }
    }

    void FindTargetField()
    {
        if (target == Target.EnemyFighter)
        {
            DuelField[] duelFields = FindObjectsOfType<DuelField>();

            foreach (DuelField duelField in duelFields)
            {
                var fieldComponent = duelField.GetComponent<Field>();
                if (fieldComponent.GetUserOfField() != userOfCard)
                {
                    targetField = duelField;
                    return;
                }
            }
        } 
        else if (target == Target.EnemyHand)
        {
            Debug.LogError("Target: EnemyHand - Not implemented");
        }        
    }
}
