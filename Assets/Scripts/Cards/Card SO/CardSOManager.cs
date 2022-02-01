using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardSOManager : MonoBehaviour
{
    public enum TargetType { EnemyFighter, EnemyBench }
    public enum DestroyType { Discard, Permanent, Capture }

    [Header("Debug Only")]
    [SerializeField] Card cardSO;
    [SerializeField] int cardPriority;
    [SerializeField] int hitPoints;

    CardContainer targetDiscard;
    CardContainer myDiscard;

    CardObjectManager COM;

    void Start()
    {
        //Debug.Log("Start at " + gameObject.name);
        hitPoints = cardSO.cardHitpoints;
        COM = GetComponent<CardObjectManager>();
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

    public void ReceiveDamage(int damage, GameObject attacker, DestroyType destroyType)
    {
        hitPoints -= damage;
        Debug.Log("Card " + gameObject.name + " received damage of: " + damage + " from:" + attacker.name);
        destroyType = CompareFighters(damage); // this method is only for War&Sacrifce
        DestroyHandler(destroyType);

    }

    DestroyType CompareFighters(int enemyCardValue) // this method is only for War&Sacrifce
    {
        Ability myCardAbility = cardSO.GetAbility(TurnManager.DuelSubphases.Offensive);
        int myFinalAbilityValue = cardSO.GetAbilityFinalValue(myCardAbility);

        if (enemyCardValue > myFinalAbilityValue)
        {
            return DestroyType.Permanent;
        }

        return DestroyType.Discard;

    }

    void DestroyHandler(DestroyType destroyType = DestroyType.Discard)
    {
        if (hitPoints > 0) return;
        if (!COM) COM = GetComponent<CardObjectManager>();

        FindDiscards();

        if (destroyType == DestroyType.Permanent)
        {
            COM.DestroyThisCardObject();
            Debug.Log(gameObject.name + " permanently destroyed");
            return;
        }

        if (destroyType == DestroyType.Capture)
        {
            if (!targetDiscard) FindDiscards();
            targetDiscard.AddCardSOToContainer(cardSO);
            Debug.Log(gameObject.name + " captured");
            COM.DestroyThisCardObject();
            return;
        }

        // then destroyType = Discard
        if (!myDiscard) FindDiscards();
        myDiscard.AddCardSOToContainer(cardSO);
        Debug.Log(gameObject.name + " discarded");
        COM.DestroyThisCardObject();
    }

    private void FindDiscards()
    {
        var decks = FindObjectsOfType<Deck>();
        
        foreach (Deck deck in decks)
        {
            var discardContainer = deck.GetComponent<DrawCards>().GetDiscardCardContainer();
            if (discardContainer.GetUserOfContainer() == COM.GetUserOfCard())
            {
                myDiscard = discardContainer;
            }
            else
            {
                targetDiscard = discardContainer;
            }
        }
    }

    public void CM_TriggerAbility(TurnManager.DuelSubphases subphase)
    {
        cardSO.TriggerAbility(subphase, gameObject);
    }

    public DuelField FindTargetField(TargetType target)
    {
        if (target == TargetType.EnemyFighter)
        {
            DuelField[] duelFields = FindObjectsOfType<DuelField>();

            foreach (DuelField duelField in duelFields)
            {
                var fieldComponent = duelField.GetComponent<Field>();
                if(!COM) COM = GetComponent<CardObjectManager>();
                if (fieldComponent.GetUserOfField() != COM.GetUserOfCard())
                {
                    return duelField;
                }
            }
        }
        else if (target == TargetType.EnemyBench)
        {
            Debug.LogError("Target: EnemyBench - Not yet implemented");
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
