using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject playerField;

    int playerCardsInField;

    public int GetPlayerCardsInField()
    {
        playerCardsInField = CountChildren(playerField);

        return playerCardsInField;
    }
    private int CountChildren(GameObject parentObject)
    {
        var numberOfChildren = parentObject.transform.childCount;

        return numberOfChildren;
    }
}
