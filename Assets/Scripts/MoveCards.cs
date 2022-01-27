using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCards : MonoBehaviour
{
    CardManager cardManager;
    void Start()
    {
        cardManager = GetComponent<CardManager>();
    }

    public bool AttemptToChangePlaceOfCard(GameObject dropZone)
    {
        var isOccupied = dropZone.gameObject.GetComponent<Field>().CheckIfFieldLimitReached();

        if (isOccupied)
        {
            //unsuccesful placing - Field is occupied
            return false;
        }

        //succesful placing
        ChangePlaceOfCard(dropZone);
        return true;
    }

    void ChangePlaceOfCard(GameObject dropZone)
    {
        transform.SetParent(dropZone.transform, false);
        transform.localPosition = Vector3.zero;

        //check if dropZone have container and if yes, the put CardSO there and detroy Object
        var cardContainer = dropZone.GetComponent<CardContainer>();
        if (cardContainer)
        {
            var cardSO = GetComponent<CardManager>().GetCardSO();
            cardContainer.AddCardSOToContainer(cardSO);
            Destroy(gameObject);
        }
    }
}
