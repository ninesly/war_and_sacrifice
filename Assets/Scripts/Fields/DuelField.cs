using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelField : Field
{
    GameSession gameSession;
    ButtonManager buttonManager;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        buttonManager = gameSession.GetComponent<ButtonManager>();
    }

    void Update()
    {
        CheckIfFighterIsChosen();
    }

    void CheckIfFighterIsChosen()
    {
        if (gameSession.GetActualUser() != user) return;
        buttonManager.SetNextTurnButton(CheckIfFieldLimitReached());
    }
}
