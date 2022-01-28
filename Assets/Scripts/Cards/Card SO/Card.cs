using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName = 1.ToString();
    public int cardHitpoints = 100;
    public Ability ability;

    public int CountCardValue()
    {
        int value = ability.GetValue() * cardHitpoints;
        return value;
    }

}
