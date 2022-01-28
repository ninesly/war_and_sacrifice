using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelField : Field
{
    TurnManager turnManager;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }

    void Update()
    {
        CheckIfFighterIsChosen();
    }

    void CheckIfFighterIsChosen()
    {
        if (turnManager.GetActualUser() != user) return;
        turnManager.SetUserAsReady(CheckIfFieldLimitReached());
    }
}
