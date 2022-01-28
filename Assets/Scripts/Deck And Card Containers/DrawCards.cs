using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] TurnManager.Users user;
    [SerializeField] CardContainer originCardContainer;
    [SerializeField] Field targetField;
    [SerializeField] bool takeCardsFromDiscardIfEmpty = false;
    [SerializeField] CardContainer discardCardContainer;
    [SerializeField] GameObject cardObject;

    int cardsToDraw;

    TurnManager gameSession;
    Croupier croupier;
    

    void Start()
    {
        gameSession = FindObjectOfType<TurnManager>();
        croupier = GetComponent<Croupier>();
    }

    public void OnClick()
    {
        var cardsInField = targetField.GetCardsInField();
        var cardsInFieldLimit = targetField.GetCardsCardsLimit();
        cardsToDraw = cardsInFieldLimit - cardsInField;

        Debug.Log("Draw " + cardsToDraw + " cards");
        DrawCardsFromContainer(cardsToDraw);
    }

    void DrawCardsFromContainer(int amount)
    {
        if (gameSession.GetActualUser() != user) return;

        for (int cardToPick = 0; cardToPick < amount; cardToPick++)
        {
            Card nextCardSO = originCardContainer.GetCardSOFromContainer();

            if (!nextCardSO)
            {
               // Debug.Log("There is no cards in the container " + originCardContainer.name);

                if (takeCardsFromDiscardIfEmpty)
                {
                    //Debug.Log("Shuffle cards from discard");
                    croupier.ShuffleAllBackToContainer(discardCardContainer, originCardContainer);
                    nextCardSO = originCardContainer.GetCardSOFromContainer();

                    if (!nextCardSO)
                    {
                       // Debug.Log("There is no cards neither in the container " + originCardContainer.name + "nor discard pile.");
                        return;
                    }
                }
                else return;
            }

            CreateCardObject(nextCardSO);
        }
    }

    void CreateCardObject(Card cardSOToCreate)
    {
        GameObject newCard = Instantiate(cardObject, Vector3.zero, Quaternion.identity);
        newCard.transform.SetParent(targetField.transform, false);
        string cardName = user + " Card " + cardSOToCreate.cardStrength;
        newCard.gameObject.name = cardName;
        newCard.GetComponent<CardObjectManager>().SetUserOfCard(user);

        // tie card object to scriptable object
        newCard.GetComponent<CardSOManager>().SetCardSO(cardSOToCreate, user);
    }
}
