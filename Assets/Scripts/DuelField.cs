using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelField : MonoBehaviour
{
    [SerializeField] GameSession.Users user;
    GameSession gameSession;
    ButtonManager buttonManager;
    Field field;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        buttonManager = gameSession.GetComponent<ButtonManager>();
        field = GetComponent<Field>();
    }

    void Update()
    {
        CheckIfFighterIsChosen();
    }

    void CheckIfFighterIsChosen()
    {
        if (gameSession.GetActualUser() != user) return;
        buttonManager.SetNextTurnButton(field.CheckIfFieldLimitReached());
    }
}
