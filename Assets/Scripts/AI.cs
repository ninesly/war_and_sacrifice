using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] GameSession.Users user;
    [SerializeField] bool isAlive = true;

    [Header("AI rig")]
    [SerializeField] CardContainer enemyDeck;
    [SerializeField] Field enemyMainField;
    [SerializeField] Field enemyDuelField;
    [SerializeField] Field enemySacrificeField;
    [SerializeField] CardContainer enemyDiscard;

    GameSession gameSession;
    DrawCards drawCardsComponent;

    void Start()
    {
        gameObject.SetActive(isAlive);
        gameSession = FindObjectOfType<GameSession>();
        drawCardsComponent = enemyDeck.GetComponent<DrawCards>();
    }

    private void Update()
    {
        Act();
    }

    private void Act()
    {
        if (gameSession.GetActualUser() != user) return;
        Debug.Log("Aimy: Hey, it's my turn! yupee!");
        drawCardsComponent.OnClick();
        var card = enemyMainField.GetComponentInChildren<DragDrop>();
        if (!card)
        {
            Debug.LogError("Aimy: I dont have cards in hand to play!");
            return;
        }
        card.ChangePlaceOfCard(enemyDuelField.gameObject);

        gameSession.NextTurn();
    }
}
