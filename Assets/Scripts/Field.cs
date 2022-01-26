using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [Tooltip("0 for no limit")]
    [SerializeField] int cardsLimitPerUser;

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
