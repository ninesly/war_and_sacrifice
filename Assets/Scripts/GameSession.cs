using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Header("Debug only")]
    [SerializeField] int playerCardsInField;
    [SerializeField] int playerSacrificedCards;
    [SerializeField] int enemyCardsInField;
    [SerializeField] int enemySacrificedCards;

    public void AddPlayerCardsInField(int amount) => playerCardsInField += amount;

    public int GetPlayerCardsInField()
    {
        return playerCardsInField;
    }

    public void SetPlayerSacrificedCards(int amount)
    {
        playerSacrificedCards = amount;
    }

    public void SetEnemyCardsInField(int amount)
    {
        enemyCardsInField = amount;
    }

    public void SetEnemySacrificedCards(int amount)
    {
        enemySacrificedCards = amount;
    }

}
