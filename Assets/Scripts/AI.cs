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
    [SerializeField] CardContainer enemyDiscard;

    [Header("Debug only")]
    [SerializeField] List<CardManager> cardsInHand = new List<CardManager>();

    GameSession gameSession;
    DrawCards drawCardsComponent;

    void Start()
    {
        gameObject.SetActive(isAlive);
        gameSession = FindObjectOfType<GameSession>();
        drawCardsComponent = enemyDeck.GetComponent<DrawCards>();
    }

    private void Update()
    {
        Act();
    }

    private void Act()
    {
        if (gameSession.GetActualUser() != user) return;
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
        var firstCard = enemyMainField.GetComponentInChildren<CardManager>();
        PlayCard(firstCard, enemyDuelField);
    }

    void SortMyCards()
    {
        
        CardManager[] allCardsObjects = enemyMainField.GetComponentsInChildren<CardManager>();
        
        foreach (CardManager card in allCardsObjects)
        {
            cardsInHand.Add(card);
        }

        cardsInHand.Sort(SortFunc);
    }

    void FightWithStrongestCard()
    {
        PlayCard(0, enemyDuelField);
    }

    void SacrificeNextStrongestCard()
    {        
        PlayCard(0, enemySacrificeField);
    }

    void DiscardWeakestCard()
    {
        var lastIndex = cardsInHand.Count - 1;
        PlayCard(lastIndex, enemyDiscard);
    }

    // -------------------------------------------------------------------------------- OTHER

    int SortFunc (CardManager cardA, CardManager cardB)
    {
        int valueA = cardA.GetCardSO().cardValue;
        int valueB = cardB.GetCardSO().cardValue;


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

    void PlayCard(int cardIndex, Field field)
    {
        var card = cardsInHand[cardIndex];
        if (!card)
        {
            Debug.LogError("Aimy: I dont have card to play!");
            return;
        }
        cardsInHand.RemoveAt(cardIndex);

        var dragDrop = card.GetComponent<DragDrop>();   
        dragDrop.ChangePlaceOfCard(field.gameObject);
    }
    void PlayCard(int cardIndex, CardContainer container)
    {
        var card = cardsInHand[cardIndex];
        if (!card)
        {
            Debug.LogError("Aimy: I dont have card to play!");
            return;
        }
        cardsInHand.RemoveAt(cardIndex);

        var dragDrop = card.GetComponent<DragDrop>();
        dragDrop.ChangePlaceOfCard(container.gameObject);
    }
    void PlayCard(CardManager card, Field field)
    {
        if (!card)
        {
            Debug.LogError("Aimy: I dont have card to play!");
            return;
        }

        var dragDrop = card.GetComponent<DragDrop>();
        dragDrop.ChangePlaceOfCard(field.gameObject);
    }
}
