using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class Meteorite : MonoBehaviour, IStateObject, IPointerClickHandler
{
    public eMeteoriteState State;
    public Transform TransformRoot = null;

    [SerializeField] private GameObject holePrefab;
    
    private MeteoriteStateContext MeteoriteStateContext = new MeteoriteStateContext();
    private int hitCount;

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
        RaycastHit2D[] hits = Physics2D.RaycastAll(TransformRoot.position, Vector2.down, 0.01f, LayerMask.GetMask("Ground"));
        return hits.Length > 0;
    }

    public void SpawnHole()
    {
        Instantiate(holePrefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (MeteoriteStateContext.GetState() != eMeteoriteState.StuckGround
            && other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        hitCount++;
        if (hitCount == 3)
            SetState(eMeteoriteState.FallDown);     
    }

    #region IPointerClickHandler
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        MeteoriteStateContext.OnPointerClick(pointerEventData);
    }
    #endregion
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        ((IMeteoriteState)State).OnPointerClick(pointerEventData);
    }
}

public abstract class IMeteoriteState : IState
{
    public IMeteoriteState(Meteorite meteorite) : base(meteorite)
    {
    }

    public virtual eMeteoriteState State { get { return eMeteoriteState.None; } }
    protected Meteorite Meteorite { get { return (Meteorite)StateObject; } }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
    }
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

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        Meteorite.SetState(eMeteoriteState.FallDownGround);
    }
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
        if (Meteorite.CheckGround())
        {
            Meteorite.SetState(eMeteoriteState.StuckGround);
            Meteorite.SpawnHole();
            GameEventManager.Instance.OnDinoFall();
        }
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

    public override void StateUpdate()
    {
        
    }
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