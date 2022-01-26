using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public enum Users { Player, Enemy };

    [SerializeField] ButtonManager buttonManager;
    [SerializeField] Users startingUser;
    [Tooltip("If true, it will override 'startingUser' selection")]
    [SerializeField] bool randomStartingUser;
    [SerializeField] int numberOfPlayer = 2;

    [Header("Debug Only")]
    [SerializeField] Field[] fieldArray;
    [SerializeField] int playerTurn = 0;
    [SerializeField] int enemyTurn = 0;
    [SerializeField] Users whoIsPlaying;

    int cardsInField;

    void Start()
    {
        fieldArray = FindObjectsOfType<Field>();
        StartGame();
    }

    void StartGame()
    {
        if (randomStartingUser)
        {
            var coinThrow = Random.Range(0, 2);
            startingUser = (Users)coinThrow;
        }
        whoIsPlaying = startingUser;
        TurnCounter(whoIsPlaying);
    }

    void TurnCounter(Users actualUser)
    {
        if ((int)actualUser == 0)
        {
            playerTurn++;
            return;
        }

        enemyTurn++;
    }

    public Users GetActualUser()
    {
        return whoIsPlaying;
    }


    public void NextPlayer()
    {
        var nextPlayer_Int = (int)whoIsPlaying + 1;
        if (nextPlayer_Int >= numberOfPlayer) nextPlayer_Int = 0;
        whoIsPlaying = (Users)nextPlayer_Int;
        TurnCounter(whoIsPlaying);

        if (whoIsPlaying == Users.Player)
        {
            buttonManager.SetButtons(true);
            return;
        }
        buttonManager.SetButtons(false);
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
    int CountChildren(Field parentObject)
    {
        var numberOfChildren = parentObject.transform.childCount;

        return numberOfChildren;
    }
}
