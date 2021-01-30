using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Catus : MonoBehaviour, IStateObject
{
    public eCatusState State;
    public Transform TransformRoot = null;
    public AnimationCurve AnimationCurveGrow = null;
    public BoxCollider2D BoxCollider2DThis = null;
    public ContactFilter2D ContactFilter2DPlayer;

    private CatusStateContext CatusStateContext = new CatusStateContext();

    void Start()
    {
        Script.Game.GameEventManager.Instance.CactusCompleted += () => 
        {
            if (State != eCatusState.Hide)
            {
                return;
            }
            SetState(eCatusState.Grow);
        };

        SetState(eCatusState.Hide);
    }

    void Update()
    {
        CatusStateContext.StateUpdate();
        State = CatusStateContext.GetState();
    }

    public void SetState(eCatusState eCatusState)
    {
        switch (eCatusState)
        {
            case eCatusState.Hide:
                CatusStateContext.SetState(new CatusState_Hide(this));
                break;
            case eCatusState.Grow:
                CatusStateContext.SetState(new CatusState_Grow(this));
                break;
            case eCatusState.Idle:
                CatusStateContext.SetState(new CatusState_Idle(this));
                break;
        }
    }

    private List<Collider2D> Collider2DsPlayer = new List<Collider2D>();
    public Dragon CheckTouchDragon()
    {
        if (BoxCollider2DThis.OverlapCollider(ContactFilter2DPlayer, Collider2DsPlayer) > 0)
        {
            return Collider2DsPlayer[0].GetComponent<Dragon>();
        }
        return null;
    }
}

public enum eCatusState
{
    None,
    Hide,
    Grow,
    Idle,
}

public class CatusStateContext : IStateContext
{
    public eCatusState GetState()
    {
        if (State == null)
        {
            return eCatusState.None;
        }
        return ((ICatusState)State).State;
    }
}

public abstract class ICatusState : IState
{
    public ICatusState(Catus catus) : base(catus)
    {
    }

    public virtual eCatusState State { get { return eCatusState.None; } }
    protected Catus Catus { get { return (Catus)StateObject; } }
}

public class CatusState_Hide : ICatusState
{
    public CatusState_Hide(Catus catus) : base(catus)
    {
    }

    public override eCatusState State { get { return eCatusState.Hide; } }

    public override void StateStart()
    {
        Catus.TransformRoot.localScale = Vector3.zero;
    }

    public override void StateUpdate() { }
    public override void StateEnd() { }
}

public class CatusState_Grow : ICatusState
{
    public CatusState_Grow(Catus catus) : base(catus)
    {
    }

    public override eCatusState State { get { return eCatusState.Grow; } }

    public override void StateStart()
    {
        Catus.TransformRoot.DOScale(Vector3.one, 0.5f).SetEase(Catus.AnimationCurveGrow).OnComplete(() =>
        {
            Catus.SetState(eCatusState.Idle);
        });
    }

    public override void StateUpdate() { }
    public override void StateEnd() { }
}

public class CatusState_Idle : ICatusState
{
    public CatusState_Idle(Catus catus) : base(catus)
    {
    }

    public override eCatusState State { get { return eCatusState.Idle; } }

    public override void StateStart()
    {
        Catus.TransformRoot.localScale = Vector3.one;
    }

    public override void StateUpdate()
    {
        CheckTouchDragon();
    }

    private Dragon DragonTouch = null;
    private void CheckTouchDragon()
    {
        DragonTouch = Catus.CheckTouchDragon();
        if (DragonTouch != null)
        {
            if (DragonTouch.State != eDragonState.Injurd)
            {
                DragonTouch.SetState(eDragonState.Injurd);
            }
            DragonTouch = null;
        }
    }

    public override void StateEnd() { }
}