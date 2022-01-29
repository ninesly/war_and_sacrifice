using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string aName = "New Ability";
    public TurnManager.DuelSubphases subphase = TurnManager.DuelSubphases.Offensive;

    public abstract void Initialize(GameObject obj);

    public abstract void TriggerAbility(GameObject obj);

    public abstract int GetValue();

    public TurnManager.DuelSubphases GetSubphase()
    {
        return subphase;
    }

}
