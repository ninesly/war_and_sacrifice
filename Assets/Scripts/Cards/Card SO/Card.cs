using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName = "1";
    public int cardHitpoints = 100;
    public Ability[] abilities;

    public int CountCardValue()
    {
        int abilitiesSumValue = 0;
        foreach (Ability ability in abilities)
        {
            abilitiesSumValue += ability.GetValue();
        }
        int value = abilitiesSumValue * cardHitpoints;
        return value;
    }

    public Ability GetAbility(TurnManager.DuelSubphases subphase)
    {
        foreach (Ability ability in abilities)
        {
            if (ability.GetSubphase() == subphase)
            {
                return ability;
            }
        }
        return null;
    }

}
