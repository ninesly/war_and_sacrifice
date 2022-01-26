using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] GameSession.Users user;
    [SerializeField] bool isAlive = true;

    GameSession gameSession;

    bool isMyTurn = false;

    void Start()
    {
        gameObject.SetActive(isAlive);
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        Act();
    }

    private void Act()
    {
        if (gameSession.GetActualUser() != user) return;
        Debug.Log("Aimy: Hey, it's my turn! yupee!");
        gameSession.NextPlayer();

    }
}
