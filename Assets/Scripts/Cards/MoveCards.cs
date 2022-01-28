using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCards : MonoBehaviour
{
    CardSOManager cardSOManager;

    private void Start()
    {
        cardSOManager = GetComponent<CardSOManager>();
    }

    public bool AttemptToChangePlaceOfCard(Field fieldForCard)
    {
        var isOccupied = fieldForCard.CheckIfFieldLimitReached();

        if (isOccupied)
        {
            //unsuccesful placing - Field is occupied
            return false;
        }

        //succesful placing
        ChangePlaceOfCard(fieldForCard);
        return true;
    }


    void ChangePlaceOfCard(Field fieldForCard)
    {
        transform.SetParent(fieldForCard.transform, false);
        transform.localPosition = Vector3.zero;

        //check if dropZone have container and if yes, the put CardSO there and detroy Object
        var cardContainer = fieldForCard.GetComponent<CardContainer>();
        if (cardContainer)
        {
            var cardSO = cardSOManager.GetCardSO();
            cardContainer.AddCardSOToContainer(cardSO);
            Destroy(gameObject);
        }
    }
}
