using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    [SerializeField] CardContainer otherCardContainer;

    CardContainer deckCardContainer;
    private void Start()
    {
        deckCardContainer = FindObjectOfType<Deck>().gameObject.GetComponent<CardContainer>();
    }
    public void ShuffleAllBackToDeck()
    {
        var allCards = otherCardContainer.GetAllCardsSOInContainer();
        for (int cardIndex = 0; cardIndex < allCards.Count; cardIndex++)
        {
            deckCardContainer.AddCardSOToContainer(allCards[cardIndex]);
        }
        deckCardContainer.ShuffleCardsInContainer();
        otherCardContainer.RemoveAllCardsSOFromContainer();
    }
}
