using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField]
    List<State> possibleTransitions = new List<State>();
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public virtual bool CanTransitionTo(Enum stateEnum)
    {
        for (int i = 0; i < possibleTransitions.Count; i++)
        {
            if (possibleTransitions[i].GetEnum().Equals(stateEnum))
            {
                return true;
            }
        }
        return false;
    }

    public abstract Enum GetEnum();
}
