using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [Tooltip("0 for no limit")]
    [SerializeField] int cardsLimit;

    int numberOfChildren;



    public bool CheckIfFieldLimitReached()
    {
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
        return cardsLimit;
    }

}
