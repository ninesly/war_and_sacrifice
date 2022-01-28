using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(MoveCards), typeof(DragDrop))]
[RequireComponent(typeof(CardZoom))]
[RequireComponent(typeof(CardSOManager))]
public class CardObjectManager : MonoBehaviour
{
    [Header("Debug Only")]
    [SerializeField] protected GameSession.Users userOfCard;

    GameSession gameSession;

    MoveCards moveCards;
    [SerializeField] TMP_Text cardText;    

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
  
        moveCards = GetComponent<MoveCards>();
        cardText = GetComponentInChildren<TMP_Text>();

        SetCardText();
    }

    void SetCardText()
    {
        var cardSO = GetComponent<CardSOManager>().GetCardSO();
        if (!cardText)
        {
            Debug.LogWarning("There is no text component on " + gameObject.name);
            return;
        }
        cardText.text = cardSO.cardStrength.ToString();
    }

    public void SetUserOfCard(GameSession.Users userOfCard)
    {
        this.userOfCard = userOfCard;
    }

    public bool OrderToChangePlaceOfCard(Field fieldForCard)
    {
        var moveCards = GetComponent<MoveCards>();
        if (!moveCards)
        {
            Debug.LogError("There is no move cards");
            return false;
        }
        return moveCards.AttemptToChangePlaceOfCard(fieldForCard);
    }



    public GameSession.Users GetActualUser()
    {
        return gameSession.GetActualUser();
    }

    public GameSession.Users GetUserOfCard()
    {
        return userOfCard;
    }
}
