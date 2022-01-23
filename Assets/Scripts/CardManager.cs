using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField] Card cardSO;

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

    public void SetCardSO(Card newCard)
    {
        cardSO = newCard;
        SetCardText();
    }


    public Card GetCardSO()
    {
        return cardSO;
    }
}
