using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] protected TurnManager.Users user;
    [Tooltip("0 for no limit")]
    [SerializeField] protected int cardsLimit;   

    public bool CheckIfFieldLimitReached()
    {
        if (cardsLimit == 0) return false;

        int numberOfChildren = transform.childCount;
        if (numberOfChildren >= cardsLimit)
        {
            return true;
        }

        return false;
    }

    public int GetCardsCardsLimit()
    {
        return cardsLimit;
    }

    public TurnManager.Users GetUserOfField()
    {
        return user;
    }

    public int GetCardsInField()
    {  
        return transform.childCount;
    }
}
