using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(DragDrop))]
[RequireComponent(typeof(CardZoom))]
[RequireComponent(typeof(CardSOManager))]
public class CardObjectManager : MonoBehaviour
{
    [Header("Debug Only")]
    [SerializeField] protected TurnManager.Users userOfCard;

    TurnManager turnManager;

    CardSOManager cardSOManager;
    TMP_Text cardText;    

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();

        cardSOManager = GetComponent<CardSOManager>();
        cardText = GetComponentInChildren<TMP_Text>();

        SetCardText();
    }

    void SetCardText()
    {
        if (!cardSOManager) cardSOManager = GetComponent<CardSOManager>();
        var cardSO = cardSOManager.GetCardSO();
        if (!cardText)
        {
            Debug.LogWarning("There is no text component on " + gameObject.name);
            return;
        }
        cardText.text = cardSO.cardName;
    }

    public void SetUserOfCard(TurnManager.Users userOfCard)
    {
        this.userOfCard = userOfCard;
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
            if (!cardSOManager) cardSOManager = GetComponent<CardSOManager>();
            var cardSO = cardSOManager.GetCardSO();
            cardContainer.AddCardSOToContainer(cardSO);
            DestroyHandler();
        }
    }

    public void DestroyHandler(CardSOManager.DestroyType destroyType = CardSOManager.DestroyType.NoGameplay)
    {
        Animator myAnimator = GetComponent<Animator>();

        if (destroyType == CardSOManager.DestroyType.Discard)
        {
            myAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            if (userOfCard == TurnManager.Users.Player)
            {
                myAnimator.SetTrigger("PlayerDiscard");  
            }
            else
            {
                myAnimator.SetTrigger("EnemyDiscard");
            }  

        }

        if (destroyType == CardSOManager.DestroyType.Permanent)
        {
            myAnimator.SetTrigger("Destroy");
            DestroyThisCardObject();
            return;
        }

        if (destroyType == CardSOManager.DestroyType.Capture)
        {
            // to implement
            DestroyThisCardObject();
            return;
        }



        DestroyThisCardObject();
    }

    void DestroyThisCardObject() // also triggered by animation
    {
        Destroy(gameObject);
    }

    public TurnManager.Users GetActualUser()
    {
        if (!turnManager)
        {
            turnManager = FindObjectOfType<TurnManager>();
        }
        return turnManager.GetActualUser();
    }

    public TurnManager.Users GetUserOfCard()
    {
        return userOfCard;
    }
}
