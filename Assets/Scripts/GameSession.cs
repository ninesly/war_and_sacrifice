using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] List<GameObject> fieldsList = new List<GameObject>();
    int cardsInField;
    

    public int GetCardsInField(GameObject targetField)
    {
        foreach(GameObject field in fieldsList)
        {
            if (targetField == field)
            {
                cardsInField = CountChildren(targetField);
                return cardsInField;
            }
        }
        Debug.LogWarning("There is no matching field!");
        return 0;
    }
    private int CountChildren(GameObject parentObject)
    {
        var numberOfChildren = parentObject.transform.childCount;

        return numberOfChildren;
    }
}
