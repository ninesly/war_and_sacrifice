using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cardsSO = new List<Card>();

    // [Header("Cards left in the deck:")]
    //[SerializeField] List<Card> remainingCards = new List<Card>();

    //int currentCardIndex = 0;
    // bool isEmpty = false;

    CardContainer cardContainer;
    private void Start()
    {
        //if (cards.Count == 0) isEmpty = true;

        cardContainer = GetComponent<CardContainer>();

        for (int cardIndex = 0; cardIndex < cardsSO.Count; cardIndex++)
        {
            cardContainer.AddCardSOToContainer(cardsSO[cardIndex]);
        }

        cardContainer.ShuffleCardsInContainer();
    }
    /*
    public Card GetCardFromDeck()
    {
        if (isEmpty) return null;

        var currentCard = remainingCards[currentCardIndex];
        remainingCards.RemoveAt(currentCardIndex);
        currentCardIndex++;
        if (currentCardIndex >= remainingCards.Count) currentCardIndex = 0;  
        if (remainingCards.Count == 0)
        {
            GetComponent<Image>().enabled = false;
            isEmpty = true;
        }

        return currentCard;
    }*/
}
