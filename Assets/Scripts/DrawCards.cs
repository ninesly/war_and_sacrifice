using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] GameSession.Users user;
    [SerializeField] CardContainer originCardContainer;
    [SerializeField] Field targetField;
    [SerializeField] bool takeCardsFromDiscardIfEmpty = false;
    [SerializeField] CardContainer discardCardContainer;
    [SerializeField] CardObjectManager cardTemplate;

    int cardsToDraw;

    GameSession gameSession;
    Croupier croupier;
    

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        croupier = GetComponent<Croupier>();
    }

    public void OnClick()
    {
        var cardsInField = gameSession.GetCardsInField(targetField);
        var cardsInFieldLimit = targetField.GetCardsCardsLimit();
        cardsToDraw = cardsInFieldLimit - cardsInField;

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
                Debug.Log("There is no cards in the container " + originCardContainer.name);

                if (takeCardsFromDiscardIfEmpty)
                {
                    Debug.Log("Shuffle cards from discard");
                    croupier.ShuffleAllBackToContainer(discardCardContainer, originCardContainer);
                    nextCardSO = originCardContainer.GetCardSOFromContainer();

                    if (!nextCardSO)
                    {
                        Debug.Log("There is no cards neither in the container " + originCardContainer.name + "nor discard pile.");
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
        CardObjectManager newCard = Instantiate(cardTemplate, Vector3.zero, Quaternion.identity);
        newCard.transform.SetParent(targetField.transform, false);
        string cardName = user + " Card " + cardSOToCreate.cardStrength;
        newCard.gameObject.name = cardName;
        newCard.SetUserOfCard(user);

        // tie card object to scriptable object
        newCard.SetCardSO(cardSOToCreate, user);
    }
}
