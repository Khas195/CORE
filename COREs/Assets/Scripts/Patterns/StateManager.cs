using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    State currentState = null;
    [SerializeField]
    [ReadOnly]
    List<State> availableStates = new List<State>();
    void Start()
    {
        currentState = null;
        FindAvailableStates();

    }
    [Button("Find Available States")]
    private void FindAvailableStates()
    {
        availableStates.Clear();

        var states = this.GetComponentsInChildren<State>();

        if (states.Length <= 0)
        {
            LogHelper.GetInstance(forceAddInEditor: true).LogWarning(this + " has no available states to operate");
            return;
        }

        availableStates.AddRange(states);


        RequestState(availableStates[0].GetEnum());
    }
    public void RequestState(Enum requestedStateEnum)
    {
        var requestedState = LookUpAvailableState(requestedStateEnum);
        if (requestedState == null)
        {
            LogHelper.GetInstance(forceAddInEditor: true).LogWarning(this + " trying to request a NULL state");
            return;
        }
        if (currentState == null)
        {
            CompleteTransition(ref currentState, requestedState);
        }
        else
        {
            if (currentState.CanTransitionTo(requestedState.GetEnum()))
            {
                CompleteTransition(ref currentState, requestedState);
            }
            else
            {
                LogHelper.GetInstance(forceAddInEditor: true).LogWarning(this + " trying to make an unsupported transition to " + requestedState);
                return;
            }
        }
    }

    public State LookUpAvailableState(Enum requestedStateEnum)
    {
        State result = null;
        for (int i = 0; i < availableStates.Count; i++)
        {
            var curEnum = availableStates[i].GetEnum();
            if (curEnum.Equals(requestedStateEnum))
            {
                result = availableStates[i];
                break;
            }
        }
        return result;
    }

    private void CompleteTransition(ref State currentState, State requestedState)
    {
        if (currentState)
        {
            currentState.OnStateExit();
        }
        if (requestedState)
        {
            requestedState.OnStateEnter();
        }
        currentState = requestedState;
        LogHelper.GetInstance(forceAddInEditor: true).LogWarning("Complete Transition from " + currentState + " to " + requestedState);
    }
    public State GetCurrentState()
    {
        return currentState;
    }
}
