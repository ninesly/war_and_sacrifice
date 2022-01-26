using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public int cardValue = 1;
    /*[Header("Debug Only")]
    public GameSession.Users userType;

    public void SetUser(GameSession.Users user)
    {
        userType = user;
    }*/

}
