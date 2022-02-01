using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Basic template for abilities
 * Then create another SO that inherit from this class. It will specify data for ability type (i.e. AttackAbility that deals dmg)
 * Then there should be script that says HOW ability works (i.e. DealDamageTriggerable) which is tied to the mechanic of a specific game.
 * Then instances of specific type of Ability have to be attached to an Ability Owner (i.e. from AttackAbility create SO of Fireball ability, then add it to Mag class/character)
 * Owner can be SO, but for a runtime it needs an Object form (f.e. Card SO attached to Card Game Object)
 * Eventual Owner SO can alter ability properties (i.e. Card SO can have stronger version of specific ability)
 * Owner Game Object yields information that can be altered during run time - like actual hitpoints or temporary powerups (in this game it's job of CardSOManager).
 * Last thing needed for whole Ability System is script that will chose fighter and run a proper ability on him (in this game - TurnManager)
 */

public abstract class Ability : ScriptableObject
{
    public string aName = "New Ability";
    public TurnManager.DuelSubphases subphase = TurnManager.DuelSubphases.Offensive; // this is an element that needs to be updated according to a game mechanic, important for TurnManager script

    public abstract void Initialize(GameObject obj, int damage);

    public abstract void TriggerAbility(GameObject obj);

    public abstract int GetValue();

    public abstract TurnManager.DuelSubphases GetSubphase();

}
