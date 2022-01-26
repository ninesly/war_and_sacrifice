using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public enum Users { Player, Enemy };

    public int cardValue = 1;
    [Header("Debug Only")]
    public Users userType;

    public void SetUser(Users user)
    {
        userType = user;
    }

}
