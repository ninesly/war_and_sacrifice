using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSOManager : CardObjectManager
{
    public enum Target { EnemyFighter, EnemyHand }

    [SerializeField] Card cardSO;

    public Card GetCardSO()
    {
        return cardSO;
    }

    public int GetCardValue()
    {
        return cardSO.CountCardValue();
    }

    public void SetCardSO(Card newCard)
    {
        cardSO = newCard;
    }

    public void ReceiveDamage(int damage, GameObject attacker)
    {
        Debug.Log("Card " + gameObject.name + " received damage of: " + damage + " from:" + attacker.name);
    }

    public void CM_TriggerAbility(TurnManager.DuelSubphases subphase)
    {
        var ability = cardSO.GetAbility(subphase);
        ability.Initialize(gameObject);
        ability.TriggerAbility(gameObject);
    }

    public DuelField FindTargetField(Target target)
    {
        if (target == Target.EnemyFighter)
        {
            DuelField[] duelFields = FindObjectsOfType<DuelField>();

            foreach (DuelField duelField in duelFields)
            {
                var fieldComponent = duelField.GetComponent<Field>();
                if (fieldComponent.GetUserOfField() != userOfCard)
                {
                    return duelField;
                }
            }
        }
        else if (target == Target.EnemyHand)
        {
            Debug.LogError("Target: EnemyHand - Not yet implemented");
            return null;
        }

        Debug.LogError(gameObject.name + " didn't specify target");
        return null;
    }

    public CardSOManager FindTargetCard(DuelField targetField)
    {
        if (!targetField) return null;

        var target_CardSOManager = targetField.GetComponentInChildren<CardSOManager>();
        if (!target_CardSOManager)
        {
            Debug.LogError(gameObject.name + " can't find target card on " + targetField.gameObject.name);
            return null;
        }

        return target_CardSOManager;
    }

}
