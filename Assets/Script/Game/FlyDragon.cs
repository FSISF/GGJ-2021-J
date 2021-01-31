using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyDragon : MonoBehaviour, IStateObject, IPointerClickHandler
{
    public eFlyDragonState State;
    public Transform TransformRoot = null;

    public SpriteRenderer SpriteRendererFlyDragon = null;
    public Animator AnimatorFlyDragon = null;

    public CameraController CameraController = null;

    private Vector2 faceDirect = Vector2.right;
    public Vector2 FaceDirect
    {
        get
        {
            return faceDirect;
        }
        private set
        {
            faceDirect = value;
            SpriteRendererFlyDragon.flipX = faceDirect.x > 0;
        }
    }

    private FlyDragonStateContext FlyDragonStateContext = new FlyDragonStateContext();

    void Start()
    {
        CameraController = GameObject.Find("Main Camera")?.GetComponent<CameraController>();

        FaceDirect = (Random.value > 0.5f) ? Vector2.right : Vector2.left;
        SetState(eFlyDragonState.Wander);
    }

    void Update()
    {
        FlyDragonStateContext.StateUpdate();
        State = FlyDragonStateContext.GetState();
    }

    public void Move(Vector3 direct)
    {
        TransformRoot.position += direct * 10 * Time.deltaTime;
        FaceDirect = direct;
    }

    public void SetState(eFlyDragonState eFlyDragonState)
    {
        switch (eFlyDragonState)
        {
            case eFlyDragonState.Wander:
                FlyDragonStateContext.SetState(new FlyDragonState_Wander(this));
                break;
            case eFlyDragonState.Hide:
                FlyDragonStateContext.SetState(new FlyDragonState_Hide(this));
                break;
        }
    }

    public void SetFlyAwayState(bool isClickRight)
    {
        FlyDragonStateContext.SetState(new FlyDragonState_FlyAway(this, isClickRight));
    }

    #region IPointerClickHandler
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        FlyDragonStateContext.OnPointerClick(pointerEvent);
    }
    #endregion
}

public enum eFlyDragonState
{
    None,
    Wander,
    FlyAway,
    Hide,
}

public class FlyDragonStateContext : IStateContext
{
    public eFlyDragonState GetState()
    {
        if (State == null)
        {
            return eFlyDragonState.None;
        }
        return ((IFlyDragonState)State).State;
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        ((IFlyDragonState)State).OnPointerClick(pointerEvent);
    }
}

public abstract class IFlyDragonState : IState
{
    public IFlyDragonState(FlyDragon flyDragon) : base(flyDragon)
    {
    }

    public virtual eFlyDragonState State { get { return eFlyDragonState.None; } }
    public FlyDragon FlyDragon { get { return (FlyDragon)StateObject; } }

    public virtual void OnPointerClick(PointerEventData pointerEvent)
    {
    }
}

public class FlyDragonState_Wander : IFlyDragonState
{
    private const float WanderXLimit = 10f;

    public FlyDragonState_Wander(FlyDragon flyDragon) : base(flyDragon)
    {
    }

    public override eFlyDragonState State { get { return eFlyDragonState.Wander; } }

    public override void StateStart() { }

    public override void StateUpdate()
    {
        if (FlyDragon.FaceDirect == Vector2.right)
        {
            FlyDragon.Move(Vector2.right);
            if (FlyDragon.TransformRoot.position.x >= WanderXLimit)
            {
                FlyDragon.Move(Vector2.left);
            }
        }
        if (FlyDragon.FaceDirect == Vector2.left)
        {
            FlyDragon.Move(Vector2.left);
            if (FlyDragon.TransformRoot.position.x <= -WanderXLimit)
            {
                FlyDragon.Move(Vector2.right);
            }
        }
    }

    private void DoMove(Vector3 moveDirect)
    {
    }

    public override void StateEnd() { }

    public override void OnPointerClick(PointerEventData pointerEvent)
    {
        FlyDragon.SetFlyAwayState(pointerEvent.pointerCurrentRaycast.worldPosition.x > FlyDragon.TransformRoot.position.x);
    }
}

public class FlyDragonState_FlyAway : IFlyDragonState
{
    private Vector2 MoveDirect;
    public FlyDragonState_FlyAway(FlyDragon flyDragon, bool isClickRight) : base(flyDragon)
    {
        MoveDirect = (isClickRight) ? Vector2.left : Vector2.right;
    }

    public override eFlyDragonState State { get { return eFlyDragonState.FlyAway; } }

    public override void StateStart() { }

    public override void StateUpdate()
    {
        FlyDragon.Move(MoveDirect);
        CheckHide();
    }

    private void CheckHide()
    {
        if (FlyDragon.CameraController != null)
        {
            Vector2 pos = FlyDragon.TransformRoot.position;
            float leftX = FlyDragon.CameraController.boundaryLeftBottom.x;
            float rightX = FlyDragon.CameraController.boundaryRightTop.x;
            if (pos.x <= leftX || pos.x >= rightX)
            {
                FlyDragon.SetState(eFlyDragonState.Hide);
            }
        }
    }

    public override void StateEnd() { }
}

public class FlyDragonState_Hide : IFlyDragonState
{
    public FlyDragonState_Hide(FlyDragon flyDragon) : base(flyDragon)
    {
    }

    public override eFlyDragonState State { get { return eFlyDragonState.Hide; } }

    public override void StateStart()
    {
        FlyDragon.gameObject.SetActive(false);
    }

    public override void StateUpdate() { }
    public override void StateEnd() { }
}