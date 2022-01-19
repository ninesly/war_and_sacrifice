using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCardsInArea : MonoBehaviour
{
    int numberOfChildren;

    private void Update()
    {
        CountChildren();
    }

    private void CountChildren()
    {
        numberOfChildren = transform.childCount;
    }
}
