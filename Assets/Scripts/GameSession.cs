using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Header("Debug Only")]
    [SerializeField] Field[] fieldArray;
    int cardsInField;

    private void Start()
    {
        fieldArray = FindObjectsOfType<Field>();
    }
    public int GetCardsInField(Field targetField)
    {
        foreach(Field field in fieldArray)
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
    private int CountChildren(Field parentObject)
    {
        var numberOfChildren = parentObject.transform.childCount;

        return numberOfChildren;
    }
}
