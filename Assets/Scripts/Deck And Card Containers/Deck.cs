using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Deck : MonoBehaviour
{
    [SerializeField] TurnManager.Users user;
    [SerializeField] Field benchField;
    [SerializeField] Field duelField;
    [SerializeField] List<Card> cardsSO = new List<Card>();

    CardContainer cardContainer;

    void Start()
    {
        cardContainer = GetComponent<CardContainer>();

        PrepareDeck();
    }

    void PrepareDeck()
    {
        for (int cardIndex = 0; cardIndex < cardsSO.Count; cardIndex++)
        {
            cardContainer.AddCardSOToContainer(cardsSO[cardIndex]);
        }

        cardContainer.ShuffleCardsInContainer();
    }

    public Field GetBenchField()
    {
        return benchField;
    }

    public Field GetDuelField()
    {
        return duelField;
    }

    public TurnManager.Users GetUserOfDeck()
    {
        return user;
    }
}