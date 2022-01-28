using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] TurnManager.Users user;
    [SerializeField] bool isAlive = true;

    [Header("AI rig")]
    [SerializeField] CardContainer enemyDeck;
    [SerializeField] Field enemyMainField;
    [SerializeField] DuelField enemyDuelField;
    [SerializeField] Field enemySacrificeField;
    [SerializeField] Field enemyDiscardField;

    [Header("Debug only")]
    [SerializeField] List<CardSOManager> cardsInHand = new List<CardSOManager>();

    TurnManager gameSession;
    DrawCards drawCardsComponent;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        gameObject.SetActive(isAlive);
        gameSession = FindObjectOfType<TurnManager>();
        drawCardsComponent = enemyDeck.GetComponent<DrawCards>();
    }

    void Update()
    {
        Act();
    }

    private void Act()
    {
        if (gameSession.GetActualUser() != user) return; //check if it's your turn
        Debug.Log("Aimy: Hey, it's my turn! yupee!");

        DrawCards();
        SortMyCards();
        //FightWithFirstCard(); // just for testing
        FightWithStrongestCard();
        //SacrificeNextStrongestCard();
        //DiscardWeakestCard();

        gameSession.NextTurn();
    }

    // -------------------------------------------------------------------------------- AI ACTIONS

    void DrawCards()
    {
        drawCardsComponent.OnClick();

        cardsInHand.Clear();
        CardSOManager[] allCardsObjects = enemyMainField.GetComponentsInChildren<CardSOManager>(); // take all Card's Objects

        foreach (CardSOManager card in allCardsObjects)
        {
            cardsInHand.Add(card); // make a list of Cards Objects
        }
    }

    void FightWithFirstCard()
    {
        if (EmptyHand()) return;
        CardSOManager firstCard = enemyMainField.GetComponentInChildren<CardSOManager>();
        PlayCard(firstCard, enemyDuelField);
    }

    void SortMyCards()
    {     
        cardsInHand.Sort(SortFunc); // sort it from the strongest to weakest
    }

    void FightWithStrongestCard()
    {
        if (EmptyHand()) return;
        PlayCard(cardsInHand[0], enemyDuelField);
    }

    void SacrificeNextStrongestCard()
    {
        if (EmptyHand()) return;
        PlayCard(cardsInHand[0], enemySacrificeField);
    }

    void DiscardWeakestCard()
    {
        if (EmptyHand()) return;
        var lastIndex = cardsInHand.Count - 1;
        PlayCard(cardsInHand[lastIndex], enemyDiscardField);
    }   

    // -------------------------------------------------------------------------------- OTHER

    int SortFunc (CardSOManager cardA, CardSOManager cardB)
    {
        int valueA = cardA.GetCardSO().cardStrength;
        int valueB = cardB.GetCardSO().cardStrength;


        if (valueA > valueB)
        {
            return -1;
        } 
        else if(valueB > valueA)
        {
            return 1;
        }
        return 0;
    }

    void PlayCard(CardSOManager chosenCard, Field field)
    {
        CardObjectManager cardManager = chosenCard.GetComponent<CardObjectManager>();

        if (!cardManager.AttemptToChangePlaceOfCard(field))
        {
            Debug.LogWarning("Aimy: That was unsuccesful play");
            return;
        }

        cardsInHand.Remove(chosenCard); 
    }

    bool EmptyHand()
    {
        if (cardsInHand.Count == 0)
        {
            Debug.LogError("Aimy: I dont have card to play!");
            return true;
        }

        return false;
    }
}
