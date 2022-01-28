using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
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
}
