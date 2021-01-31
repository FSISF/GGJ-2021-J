using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour, IStateObject
{
    public eMeteoriteState State;
    public Transform TransformRoot = null;

    private MeteoriteStateContext MeteoriteStateContext = new MeteoriteStateContext();

    void Start()
    {
        SetState(eMeteoriteState.Idle);
    }

    void Update()
    {
        MeteoriteStateContext.StateUpdate();
        State = MeteoriteStateContext.GetState();
    }

    public void SetState(eMeteoriteState eMeteoriteState)
    {
        switch (eMeteoriteState)
        {
            case eMeteoriteState.Idle:
                MeteoriteStateContext.SetState(new MeteoriteState_Idle(this));
                break;
            case eMeteoriteState.FallDownGround:
                MeteoriteStateContext.SetState(new MeteoriteState_FallDownGround(this));
                break;
            case eMeteoriteState.StuckGround:
                MeteoriteStateContext.SetState(new MeteoriteState_StuckGround(this));
                break;
            case eMeteoriteState.FallDown:
                MeteoriteStateContext.SetState(new MeteoriteState_FallDown(this));
                break;
        }
    }

    public void FallDown()
    {
        TransformRoot.position += Vector3.down * 10f * Time.deltaTime;
    }

    public bool CheckGround()
    {
        return false;
    }
}

public enum eMeteoriteState
{
    None,
    Idle,
    FallDownGround,
    StuckGround,
    FallDown,
}

public class MeteoriteStateContext : IStateContext
{
    public eMeteoriteState GetState()
    {
        if (State == null)
        {
            return eMeteoriteState.None;
        }
        return ((IMeteoriteState)State).State;
    }
}

public abstract class IMeteoriteState : IState
{
    public IMeteoriteState(Meteorite meteorite) : base(meteorite)
    {
    }

    public virtual eMeteoriteState State { get { return eMeteoriteState.None; } }
    protected Meteorite Meteorite { get { return (Meteorite)StateObject; } }
}

public class MeteoriteState_Idle : IMeteoriteState
{
    public MeteoriteState_Idle(Meteorite meteorite) : base(meteorite)
    {
    }

    public override eMeteoriteState State { get { return eMeteoriteState.Idle; } }

    public override void StateStart() { }
    public override void StateUpdate() { }
    public override void StateEnd() { }
}

public class MeteoriteState_FallDownGround : IMeteoriteState
{
    public MeteoriteState_FallDownGround(Meteorite meteorite) : base(meteorite)
    {
    }

    public override eMeteoriteState State { get { return eMeteoriteState.FallDownGround; } }

    public override void StateStart() { }

    public override void StateUpdate()
    {
        Meteorite.FallDown();
    }

    public override void StateEnd() { }
}

public class MeteoriteState_StuckGround : IMeteoriteState
{
    public MeteoriteState_StuckGround(Meteorite meteorite) : base(meteorite)
    {
    }

    public override eMeteoriteState State { get { return eMeteoriteState.StuckGround; } }

    public override void StateStart() { }
    public override void StateUpdate() { }
    public override void StateEnd() { }
}

public class MeteoriteState_FallDown : IMeteoriteState
{
    public MeteoriteState_FallDown(Meteorite meteorite) : base(meteorite)
    {
    }

    public override eMeteoriteState State { get { return eMeteoriteState.FallDown; } }

    public override void StateStart() { }

    public override void StateUpdate()
    {
        Meteorite.FallDown();
        if (Meteorite.TransformRoot.position.y <= -100f)
        {
            Meteorite.gameObject.SetActive(false);
        }
    }

    public override void StateEnd() { }
}