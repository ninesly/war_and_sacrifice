using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();

    [Header("Cards left in the deck:")]
    [SerializeField] List<Card> remainingCards = new List<Card>();

    int currentCardIndex = 0;

    private void Start()
    {
        remainingCards = cards;
    }
    public Card GetCardFromDeck()
    {
        var currentCard = remainingCards[currentCardIndex];
        currentCardIndex++;
        if (currentCardIndex >= remainingCards.Count) currentCardIndex = 0;
        

        return currentCard;
    }
}
