using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateContext
{
    protected IState State = null;

    public void SetState(IState state)
    {
        State?.StateEnd();
        State = state;
        State?.StateStart();
    }

    public void StateUpdate()
    {
        State?.StateUpdate();
    }
}

public abstract class IState
{
    protected IStateObject StateObject = null;
    public IState(IStateObject stateObject)
    {
        StateObject = stateObject;
    }

    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateEnd();
}

public interface IStateObject
{
}