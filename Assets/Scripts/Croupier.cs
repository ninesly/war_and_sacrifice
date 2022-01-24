using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croupier : MonoBehaviour
{
    [SerializeField] CardContainer originCardContainer;
    [SerializeField] CardContainer targetCardContainer;

    public void OnClick()
    {
        ShuffleAllBackToContainer(originCardContainer, targetCardContainer);
    }

    public void ShuffleAllBackToContainer(CardContainer originCardContainer, CardContainer targetCardContainer)
    {
        var allCards = originCardContainer.GetAllCardsSOInContainer();
        for (int cardIndex = 0; cardIndex < allCards.Count; cardIndex++)
        {
            targetCardContainer.AddCardSOToContainer(allCards[cardIndex]);
        }
        targetCardContainer.ShuffleCardsInContainer();
        originCardContainer.RemoveAllCardsSOFromContainer();
    }
}
