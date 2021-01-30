using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour, IStateObject
{
    public eDragonState State;
    public Transform TransformRoot = null;
    public Rigidbody2D Rigidbody2DThis = null;
    public BoxCollider2D BoxCollider2DThis = null;

    public ContactFilter2D ContactFilter2DGround;

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
            case eDragonState.Hot:
                DragonStateContext.SetState(new DragonState_Hot(this));
                break;
            case eDragonState.Injurd:
                DragonStateContext.SetState(new DragonState_Injurd(this));
                break;
        }
    }

    public void Move(Vector3 direct)
    {
        TransformRoot.position += direct * 10 * Time.deltaTime;
    }

    public void Jump()
    {
        Rigidbody2DThis.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }

    public bool CheckGround()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(TransformRoot.position, Vector2.down, BoxCollider2DThis.bounds.extents.y + 0.01f, LayerMask.GetMask("Ground"));
        return hits.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(TransformRoot.position, TransformRoot.position + (Vector3.down * (BoxCollider2DThis.bounds.extents.y + 0.01f)));
    }
}

public enum eDragonState
{
    None,
    Idle,
    Move,
    Jump,
    Hot,
    Injurd,
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Dragon.SetState(eDragonState.Move);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Dragon.SetState(eDragonState.Jump);
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
        CheckControl();
    }

    private void CheckControl()
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dragon.SetState(eDragonState.Jump);
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

    public override void StateStart()
    {
        MusicSystem.Instance.PlaySound(eSound.Jump);
        Dragon.Jump();
    }

    public override void StateUpdate()
    {
        CheckControl();

        if (Dragon.CheckGround())
        {
            Dragon.SetState(eDragonState.Idle);
        }
    }

    private void CheckControl()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Dragon.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dragon.Move(Vector3.right);
        }
    }

    public override void StateEnd() { }
}

public class DragonState_Hot : IDragonState
{
    public DragonState_Hot(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.Hot; } }

    public override void StateStart() { }
    public override void StateUpdate() { }
    public override void StateEnd() { }
}

public class DragonState_Injurd : IDragonState
{
    public DragonState_Injurd(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.Injurd; } }

    public override void StateStart()
    {
        MusicSystem.Instance.PlaySound(eSound.Hit);
    }

    public override void StateUpdate() { }
    public override void StateEnd() { }
}