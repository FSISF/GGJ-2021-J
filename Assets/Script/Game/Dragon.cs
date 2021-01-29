using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour, IStateObject
{
    public eDragonState State;
    public Transform TransformRoot = null;

    private DragonStateContext DragonStateContext = new DragonStateContext();

    void Start()
    {
        SetState(eDragonState.Idle);
    }

    void Update()
    {
        DragonStateContext.StateUpdate();
        State = DragonStateContext.GetState();
    }

    public void SetState(eDragonState eDragonState)
    {
        switch (eDragonState)
        {
            case eDragonState.Idle:
                DragonStateContext.SetState(new DragonState_Idle(this));
                break;
            case eDragonState.Move:
                DragonStateContext.SetState(new DragonState_Move(this));
                break;
            case eDragonState.Jump:
                DragonStateContext.SetState(new DragonState_Jump(this));
                break;
        }
    }

    public void Move(Vector3 direct)
    {
        TransformRoot.position += direct * 10 * Time.deltaTime;
    }
}

public enum eDragonState
{
    None,
    Idle,
    Move,
    Jump,
}

public class DragonStateContext : IStateContext
{
    public eDragonState GetState()
    {
        if (State == null)
        {
            return eDragonState.None;
        }
        return ((IDragonState)State).State;
    }
}

public abstract class IDragonState : IState
{
    public IDragonState(Dragon dragon) : base(dragon)
    {
    }

    public virtual eDragonState State { get { return eDragonState.None; } }
    protected Dragon Dragon { get { return (Dragon)StateObject; } }
}

public class DragonState_Idle : IDragonState
{
    public DragonState_Idle(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.Idle; } }

    public override void StateStart() { }
    public override void StateUpdate()
    {
        CheckMove();
    }

    private void CheckMove()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            Dragon.SetState(eDragonState.Move);
        }
    }

    public override void StateEnd() { }
}

public class DragonState_Move : IDragonState
{
    public DragonState_Move(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.Move; } }

    public override void StateStart() { }

    public override void StateUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Dragon.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dragon.Move(Vector3.right);
        }
        else
        {
            Dragon.SetState(eDragonState.Idle);
        }
    }

    public override void StateEnd() { }
}

public class DragonState_Jump : IDragonState
{
    public DragonState_Jump(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.Jump; } }

    public override void StateStart() { }
    public override void StateUpdate() { }
    public override void StateEnd() { }
}