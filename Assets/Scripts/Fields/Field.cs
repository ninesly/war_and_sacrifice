using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] protected GameSession.Users user;
    [Tooltip("0 for no limit")]
    [SerializeField] int cardsLimit;

    int numberOfChildren;


    public bool CheckIfFieldLimitReached()
    {
        Debug.Log("CheckIfFieldLimitReached");

        if (cardsLimit == 0) return false;

        numberOfChildren = transform.childCount;
        if (numberOfChildren >= cardsLimit)
        {
            return true;
        }

        return false;
    }

    public int GetCardsCardsLimit()
    {
        Debug.Log("GetCardsCardsLimit");
        return cardsLimit;
    }

    public GameSession.Users GetUserOfField()
    {
        Debug.Log("GetUserOfField");
        return user;
    }

     
}
