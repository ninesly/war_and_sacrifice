using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCardsInField : MonoBehaviour
{
    enum Users { Player, Enemy };

    [Tooltip("0 for no limit")]
    [SerializeField] int cardsLimitPerUser;
    [SerializeField] Users userType;
    int numberOfChildren;



    public bool CheckIfFieldLimitReached()
    {
        if (cardsLimitPerUser == 0) return false;

        numberOfChildren = transform.childCount;
        if (numberOfChildren >= cardsLimitPerUser)
        {
            return true;
        }

        return false;

    }
}
