using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();

    [Header("Cards left in the deck:")]
    [SerializeField] List<Card> remainingCards = new List<Card>();

    int currentCardIndex = 0;
    bool isEmpty = false;

    private void Start()
    {
        if (cards.Count == 0) isEmpty = true;

        for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
        {
            remainingCards.Add(cards[cardIndex]);
        }
    }
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
    }
}
