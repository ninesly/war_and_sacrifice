using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName = "1";
    public int cardHitpoints = 100;
    public CardAbility[] cardAbilities;



    public int CountCardPriority()  
    {
        // AI decides which card to play according to Priority

        int abilitiesSumValue = 0;
        foreach (CardAbility cardAbility in cardAbilities)
        {
            abilitiesSumValue += GetFinalValue(cardAbility);
        }
        int priority = abilitiesSumValue * cardHitpoints;
        return priority;
    }

    int GetAbilityValue(Ability ability)
    {
        foreach (CardAbility cardAbility in cardAbilities)
        {
            if (cardAbility.ability == ability)
            {
                return GetFinalValue(cardAbility);
            }
        }
        return 0;
    }

    int GetFinalValue (CardAbility cardAbility)
    {
        if (cardAbility.valueOverride > 0)
        {
            return cardAbility.valueOverride;
        }

        int value = cardAbility.ability.GetValue() * cardAbility.valueMultiplierMod + cardAbility.valuePlusMod;
        return value;
    }

    public Ability GetAbility(TurnManager.DuelSubphases subphase)
    {
        foreach (CardAbility cardAbility in cardAbilities)
        {
            if (cardAbility.ability.GetSubphase() == subphase)
            {
                return cardAbility.ability;
            }
        }
        return null;
    }

    public void TriggerAbility(TurnManager.DuelSubphases subphase, GameObject whoIsTriggering)
    {
        foreach (CardAbility cardAbility in cardAbilities)
        {
            if (cardAbility.ability.GetSubphase() == subphase)
            {
                var cardDamage = GetAbilityValue(cardAbility.ability);
                cardAbility.ability.Initialize(whoIsTriggering, cardDamage);
                cardAbility.ability.TriggerAbility(whoIsTriggering);
                Debug.Log(whoIsTriggering.gameObject.name + " succesfully triggered its " + subphase +" ability!");
                return;
                // for now only one ability per subphase
            }            
        }
        Debug.Log(whoIsTriggering.gameObject.name + " doesn't have " + subphase + " ability.");
    }

}

[Serializable]
public class CardAbility 
{
    public Ability ability;
    public int valueOverride;
    public int valuePlusMod;
    [SerializeField][Range(1, 100)] public int valueMultiplierMod;


    public CardAbility(Ability ability, int valueOverride, int valuePlusMod, int valueMultiplierMod = 1)
    {
        this.ability = ability;
        this.valueOverride = valueOverride;
        this.valuePlusMod = valuePlusMod;
        this.valueMultiplierMod = valueMultiplierMod;
    }
}
