using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] GameSession.Users user;
    [SerializeField] bool isAlive = true;

    [Header("AI rig")]
    [SerializeField] CardContainer enemyDeck;
    [SerializeField] Field enemyMainField;
    [SerializeField] Field enemyDuelField;
    [SerializeField] Field enemySacrificeField;
    [SerializeField] Field enemyDiscardField;

    [Header("Debug only")]
    [SerializeField] List<CardObjectManager> cardsInHand = new List<CardObjectManager>();

    GameSession gameSession;
    DrawCards drawCardsComponent;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        gameObject.SetActive(isAlive);
        gameSession = FindObjectOfType<GameSession>();
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
        //PlayFirstCard(); // just for testing
        SortMyCards();
        FightWithStrongestCard();
        SacrificeNextStrongestCard();
        DiscardWeakestCard();

        gameSession.NextTurn();
    }

    // -------------------------------------------------------------------------------- AI ACTIONS

    void DrawCards()
    {
        drawCardsComponent.OnClick();
    }

    void FightWithFirstCard()
    {
        if (EmptyHand()) return;
        var firstCard = enemyMainField.GetComponentInChildren<CardObjectManager>();
        PlayCard(firstCard, enemyDuelField);
    }

    void SortMyCards()
    {        
        CardObjectManager[] allCardsObjects = enemyMainField.GetComponentsInChildren<CardObjectManager>(); // take all Card's Objects
        
        foreach (CardObjectManager card in allCardsObjects)
        {
            cardsInHand.Add(card); // make a list of Cards Objects
        }

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

    int SortFunc (CardObjectManager cardA, CardObjectManager cardB)
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

    void PlayCard(CardObjectManager chosenCard, Field field)
    {
        cardsInHand.Remove(chosenCard);

        chosenCard.AttemptToChangePlaceOfCard(field.gameObject);
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
