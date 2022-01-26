using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField] Card cardSO;
    [Header("Debug Only")]
    [SerializeField] GameSession.Users user;

    TMP_Text cardText;

    private void Start()
    {
        cardText = GetComponentInChildren<TMP_Text>();
        SetCardText();
    }

    private void SetCardText()
    {
        if (!cardSO) return;
        if (!cardText) return;

        cardText.text = cardSO.cardValue.ToString();
    }

    public void SetCardSO(Card newCard, GameSession.Users user)
    {
        cardSO = newCard;
        SetCardText();
    }


    public Card GetCardSO()
    {
        return cardSO;
    }

    public GameSession.Users GetUserOfCard()
    {
        return user;
    }
}
