using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] GameObject PlayerArea;
    [SerializeField] GameObject EnemyArea;
    [SerializeField] GameObject Card;
    [SerializeField] int cardInFieldLimit = 3;

    int cardsInField;
    int cardsToDraw;

    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public void OnClick()
    {
        cardsInField = gameSession.GetPlayerCardsInField();
        cardsToDraw = cardInFieldLimit - cardsInField;

        for (int i = 0; i < cardsToDraw; i++)
        {
            GameObject pickedCard = Instantiate(Card, Vector3.zero, Quaternion.identity);
            pickedCard.transform.SetParent(PlayerArea.transform, false);
        }

        gameSession.AddPlayerCardsInField(cardsToDraw);

    }
}
