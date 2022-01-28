using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(MoveCards), typeof(DragDrop), typeof(DealDamageTriggerable))]
[RequireComponent(typeof(CardZoom))] 
public class CardObjectManager : MonoBehaviour
{
    [SerializeField] Card cardSO;
    [Header("Debug Only")]
    [SerializeField] GameSession.Users userOfCard;

    TMP_Text cardText;

    MoveCards moveCards;
    GameSession gameSession;

    void Start()
    {
        moveCards = GetComponent<MoveCards>();
        cardText = GetComponentInChildren<TMP_Text>();

        gameSession = FindObjectOfType<GameSession>();
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

    public bool AttemptToChangePlaceOfCard(GameObject dropZone)
    {
        var result = moveCards.AttemptToChangePlaceOfCard(dropZone);
        return result;
    }

    public GameSession.Users GetActualUser()
    {
        return gameSession.GetActualUser();
    }
}
