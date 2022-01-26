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
    [SerializeField] CardManager cardTemplate;

    int cardsToDraw;

    GameSession gameSession;
    Croupier croupier;
    

    private void Start()
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

    public void DrawCardsFromContainer(int amount)
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

    private void CreateCardObject(Card cardSOToCreate)
    {
        var newCard = Instantiate(cardTemplate, Vector3.zero, Quaternion.identity);
        newCard.transform.SetParent(targetField.transform, false);

        // tie card object to scriptable object
        newCard.GetComponent<CardManager>().SetCardSO(cardSOToCreate, user);
    }
}
