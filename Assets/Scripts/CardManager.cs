using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField] Card card;

    TMP_Text cardText;

    private void Start()
    {
        cardText = GetComponentInChildren<TMP_Text>();
        cardText.text = card.cardValue.ToString();
    }

    public void SetCard(Card newCard) => card = newCard; 
}
