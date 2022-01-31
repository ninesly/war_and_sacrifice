using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSOManager : MonoBehaviour
{
    public enum Target { EnemyFighter, EnemyHand }

    [Header("Debug Only")]
    [SerializeField] Card cardSO;
    [SerializeField] int cardPriority;
    [SerializeField] int hitPoints;

    void Start()
    {
        //Debug.Log("Start at " + gameObject.name);
        hitPoints = cardSO.cardHitpoints;
    }
    public Card GetCardSO()
    {
        return cardSO;
    }

    public int GetCardPriority()
    {
        cardPriority = cardSO.CountCardPriority();
        return cardPriority;
    }

    public void SetCardSO(Card newCard)
    {
        cardSO = newCard;
    }

    public void ReceiveDamage(int damage, GameObject attacker)
    {
        hitPoints -= damage;
        Debug.Log("Card " + gameObject.name + " received damage of: " + damage + " from:" + attacker.name);
    }

    public void CM_TriggerAbility(TurnManager.DuelSubphases subphase)
    {
        cardSO.TriggerAbility(subphase, gameObject);
    }

    public DuelField FindTargetField(Target target)
    {
        if (target == Target.EnemyFighter)
        {
            DuelField[] duelFields = FindObjectsOfType<DuelField>();

            foreach (DuelField duelField in duelFields)
            {
                var fieldComponent = duelField.GetComponent<Field>();
                var COM = GetComponent<CardObjectManager>();
                if (fieldComponent.GetUserOfField() != COM.GetUserOfCard())
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
