using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    [SerializeField] Card cardSO;
    [Header("Debug Only")]
    [SerializeField] GameSession.Users userOfCard;

    TMP_Text cardText;

    void Start()
    {
        cardText = GetComponentInChildren<TMP_Text>();        
        SetCardText();
    }

    void SetCardText()
    {
        if (!cardSO) return;
        if (!cardText) return;

        cardText.text = cardSO.cardStrength.ToString();
    }

    public void SetCardSO(Card newCard, GameSession.Users user)
    {
        cardSO = newCard;
        cardSO.ability.Initialize(gameObject);
        SetCardText();
    }


    public Card GetCardSO()
    {
        return cardSO;
    }

    public GameSession.Users GetUserOfCard()
    {
        return userOfCard;
    }

    public void SetUserOfCard(GameSession.Users userOfCard)
    {
        this.userOfCard = userOfCard;
    }


    public void ReceiveDamage(int damage)
    {
        Debug.Log("Card " + gameObject.name + " received damage of: " + damage);
    }

    public void CM_TriggerAbility()
    {
        cardSO.ability.TriggerAbility(gameObject);
    }
}
