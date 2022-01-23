using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] GameObject PlayerArea;
    [SerializeField] GameObject EnemyArea;
    [SerializeField] GameObject Card;
    [SerializeField] int cardInFieldLimit = 3;

    int cardsInField;
    int cardsToDraw;

    GameSession gameSession;
    CardContainer deckCardContainer;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        deckCardContainer = GetComponent<CardContainer>();
    }

    public void OnClick()
    {
        cardsInField = gameSession.GetPlayerCardsInField();
        cardsToDraw = cardInFieldLimit - cardsInField;
        Debug.Log("Cards to draw: " + cardsToDraw);

        for (int i = 0; i < cardsToDraw; i++)
        {
            PickingCard();
        }
    }

    private void PickingCard()
    {
        Card nextCardFromDeck = deckCardContainer.GetCardSOFromContainer();
        if (!nextCardFromDeck)
        {
            Debug.Log("there is no cards in the deck");
            return;
        }

        GameObject pickedCard = Instantiate(Card, Vector3.zero, Quaternion.identity);
        pickedCard.transform.SetParent(PlayerArea.transform, false);
        pickedCard.GetComponent<CardManager>().SetCardSO(nextCardFromDeck);
    }
}
