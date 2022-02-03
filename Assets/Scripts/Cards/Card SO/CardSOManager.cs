using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardSOManager : MonoBehaviour
{
    public enum TargetType { OpponentFighter, OpponentBench, MyFighter, MyBench }
    public enum DestroyType { Discard, Permanent, Capture, NoGameplay }

    [Header("Debug Only")]
    [SerializeField] Card cardSO;
    [SerializeField] int cardPriority;
    [SerializeField] int hitPoints;
    [SerializeField] int dmgBonus;

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
        Debug.Log("Card " + gameObject.name + " received damage of: " + damage + " from:" + attacker.name);
        ChangeHealth(-damage, destroyType);
    }

    void ChangeHealth(int amount, DestroyType destroyType = DestroyType.Discard)
    {
        hitPoints += amount;
        if (hitPoints <= 0)
        {
            Debug.Log(gameObject.name + "lost all hitpoints.");
            DestroyHandler(destroyType);
        }

    }
    public void AddBonus(int bonus, GameObject supporter)
    {
        dmgBonus = bonus;
        Debug.Log(gameObject.name + " received bonus " + bonus + " from:" + supporter.name);
        supporter.GetComponent<CardObjectManager>().DestroyHandler(DestroyType.Permanent);
    }

    public void CompareFighters(int opponentHitpoints) // this method is only for War&Sacrifce
    {
        if (opponentHitpoints > hitPoints)
        {
            DestroyHandler(DestroyType.Permanent);
            return;
        }

        DestroyHandler(DestroyType.Discard);

    }

    void DestroyHandler(DestroyType destroyType)
    {
        if (!COM) COM = GetComponent<CardObjectManager>();

        FindDiscards();

        if (destroyType == DestroyType.Permanent)
        {
            COM.DestroyHandler(destroyType);
            Debug.Log(gameObject.name + " permanently destroyed");
            return;
        }

        if (destroyType == DestroyType.Capture)
        {
            if (!targetDiscard) FindDiscards();
            targetDiscard.AddCardSOToContainer(cardSO);
            Debug.Log(gameObject.name + " captured");
            COM.DestroyHandler(destroyType);
            return;
        }

        // then destroyType = Discard
        if (!myDiscard) FindDiscards();
        myDiscard.AddCardSOToContainer(cardSO);
        Debug.Log(gameObject.name + " discarded");
        COM.DestroyHandler(destroyType);
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
        cardSO.TriggerAbility(subphase, gameObject, dmgBonus);
    }

    public Field FindTargetField(TargetType target)
    {
        if (!COM) COM = GetComponent<CardObjectManager>();

        Deck[] decks = FindObjectsOfType<Deck>();

        if (target == TargetType.OpponentFighter)
        {
            foreach (Deck deck in decks)
            {
                if (deck.GetUserOfDeck() != COM.GetUserOfCard())
                {
                    Field opponentDuelField = deck.GetDuelField();
                    return opponentDuelField;
                }
            }
        }

        if (target == TargetType.MyFighter)
        {
            foreach (Deck deck in decks)
            {
                if (deck.GetUserOfDeck() == COM.GetUserOfCard())
                {
                    Field myDuelField = deck.GetDuelField();
                    return myDuelField;
                }
            }
        }

        if (target == TargetType.OpponentBench)
        {
            foreach (Deck deck in decks)
            {
                if (deck.GetUserOfDeck() != COM.GetUserOfCard())
                {
                    Field opponentBenchField = deck.GetBenchField();
                    return opponentBenchField;
                }
            }
        }

        if (target == TargetType.MyBench)
        {
            foreach (Deck deck in decks)
            {
                if (deck.GetUserOfDeck() == COM.GetUserOfCard())
                {
                    Field myBenchField = deck.GetBenchField();
                    return myBenchField;
                }
            }
        }

        Debug.LogError(gameObject.name + " didn't specify target");
        return null;
    }

    public CardSOManager FindTargetCard(Field targetField)
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


    public int GetHitpoints()
    {
        return hitPoints;
    }

}
