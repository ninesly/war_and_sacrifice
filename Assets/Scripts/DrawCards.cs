using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] CardContainer sourceCardContainer;
    [SerializeField] GameObject targetField;
    [SerializeField] int cardInFieldLimit = 3; // that shouldn't be place to define it, but for now let's leave it like that
    [SerializeField] GameObject cardTemplate;    

    int cardsInField;
    int cardsToDraw;

    GameSession gameSession;
    

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public void OnClick()
    {
        cardsInField = gameSession.GetCardsInField(targetField);
        cardsToDraw = cardInFieldLimit - cardsInField;

        DrawCardsFromContainer(cardsToDraw);
    }

    public void DrawCardsFromContainer(int amount)
    {
        for (int cardToPick = 0; cardToPick < amount; cardToPick++)
        {
            Card nextCardSO = sourceCardContainer.GetCardSOFromContainer();
            if (!nextCardSO)
            {
                Debug.Log("There is no cards in the container " + sourceCardContainer.name);
                return;
            }

            CreateCardObject(nextCardSO);
        }
    }

    private void CreateCardObject(Card cardSOToCreate)
    {
        GameObject newCard = Instantiate(cardTemplate, Vector3.zero, Quaternion.identity);
        newCard.transform.SetParent(targetField.transform, false);

        // tie card object to scriptable object
        newCard.GetComponent<CardManager>().SetCardSO(cardSOToCreate);
    }
}
