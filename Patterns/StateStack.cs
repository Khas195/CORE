using System;
using System.Collections;
using System.Collections.Generic;
public class StateStack 
{
    Stack<State> stack = new Stack<State>();

    public void Push(State newState)
    {
        if (newState == null)
        {
            Definition.StateStackDebug("Pushing NULL into the stack");
        }

        if (IsStackEmpty() == false)
        {
            Definition.StateStackDebug("Call OnPressed on " + stack.Peek());
            stack.Peek().OnPressed();
        }

        Definition.StateStackDebug("Call OnPushed on " + newState);

        newState.OnPushed();

        stack.Push(newState);
    }

    public bool IsStackEmpty()
    {
        return stack.Count <= 0;
    }

    public State Pop()
    {
        State result = null;
        if (IsStackEmpty())
        {
            Definition.StateStackDebug("Popping an emptyState");
            return result;
        }

        result = stack.Pop();
        Definition.StateStackDebug("Call OnPop on" + result);
        result.OnPopped();

        if (!IsStackEmpty())
        {
            Definition.StateStackDebug("Call OnReturn Peek on" + stack.Peek());
            stack.Peek().OnReturnPeek();
        }
        return result;
    }

    public void EmptyStack()
    {
        while (IsStackEmpty() == false)
        {
            Pop();
        }
    }

    public State GetPeek()
    {
        return stack.Peek();
    }

    public bool Contains(State playingState)
    {
        return stack.Contains(playingState);
    }
}
