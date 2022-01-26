using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cardsSO = new List<Card>();
    [SerializeField] GameSession.Users user;

    CardContainer cardContainer;

    private void Start()
    {
        cardContainer = GetComponent<CardContainer>();

        PrepareDeck();
    }

    private void PrepareDeck()
    {
        for (int cardIndex = 0; cardIndex < cardsSO.Count; cardIndex++)
        {
            cardsSO[cardIndex].SetUser(user);
            cardContainer.AddCardSOToContainer(cardsSO[cardIndex]);
        }

        cardContainer.ShuffleCardsInContainer();
    }
}
