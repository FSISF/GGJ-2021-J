using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

public class Dragon : MonoBehaviour, IStateObject
{
    public eDragonState State;
    public Transform TransformRoot = null;
    public Rigidbody2D Rigidbody2DThis = null;
    public BoxCollider2D BoxCollider2DThis = null;
    public SpriteRenderer SpriteRendererDragon = null;
    public Animator AnimatorDragon = null;

    public SunController SunController = null;

    public float moveSpeed = 10f;

    public float jumpForce = 2.5f;

    public int health = 3;

    public int maxHealth = 3;

    private DragonStateContext DragonStateContext = new DragonStateContext();

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
            SpriteRendererDragon.flipX = faceDirect.x < 0;
        }
    }

    void Start()
    {
        SunController = GameObject.Find("Sun")?.GetComponent<SunController>();

        FaceDirect = Vector2.right;
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
            case eDragonState.ReadyJump:
                DragonStateContext.SetState(new DragonState_ReadyJump(this));
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
            case eDragonState.Dead:
                DragonStateContext.SetState(new DragonState_Dead(this));
                break;
        }
    }

    public void Move(Vector2 direct)
    {
        Rigidbody2DThis.position += direct * moveSpeed * Time.deltaTime;
        FaceDirect = direct;
    }

    public void Jump()
    {
        Rigidbody2DThis.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }

    public bool CheckGround()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(TransformRoot.position, Vector2.down, BoxCollider2DThis.bounds.extents.y + 0.05f, LayerMask.GetMask("Ground", "Meteorite"));
        return hits.Length > 0 && Rigidbody2DThis.velocity.y <= 0.05f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(TransformRoot.position, TransformRoot.position + (Vector3.down * (BoxCollider2DThis.bounds.extents.y + 0.05f)));
    }
}

public enum eDragonState
{
    None,
    Idle,
    Move,
    ReadyJump,
    Jump,
    Hot,
    Injurd,
    Dead,
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

    public override void StateStart()
    {
        Dragon.AnimatorDragon.Play("Idle");
    }

    public override void StateUpdate()
    {
        CheckMove();
        CheckSun();
    }

    private void CheckMove()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Dragon.SetState(eDragonState.Move);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Dragon.SetState(eDragonState.ReadyJump);
        }
    }

    private void CheckSun()
    {
        if (Dragon.SunController != null)
        {
            if (Vector2.Distance(Dragon.SunController.transform.position, Dragon.TransformRoot.position) <= 5f)
            {
                Dragon.SetState(eDragonState.Hot);
            }
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

    public override void StateStart()
    {
        Dragon.AnimatorDragon.Play("Walk");
    }

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
            Dragon.SetState(eDragonState.ReadyJump);
        }
    }

    public override void StateEnd() { }
}

public class DragonState_ReadyJump : IDragonState
{
    public DragonState_ReadyJump(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.ReadyJump; } }

    public override void StateStart()
    {
        Dragon.AnimatorDragon.Play("Jump");
        Common.Timer(10f / 60f, () => 
        {
            Dragon.SetState(eDragonState.Jump);
        });
    }

    public override void StateUpdate() { }
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
            Dragon.Rigidbody2DThis.velocity = Vector2.zero;
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

    public override void StateStart()
    {
        Dragon.AnimatorDragon.Play("Sweat");
        Script.Game.GameEventManager.Instance.OnDinoSweat();
    }

    public override void StateUpdate()
    {
        CheckSun();
    }

    private void CheckSun()
    {
        if (Dragon.SunController != null)
        {
            if (Vector2.Distance(Dragon.SunController.transform.position, Dragon.TransformRoot.position) > 5f)
            {
                Dragon.SetState(eDragonState.Idle);
            }
        }
    }

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
        Dragon.AnimatorDragon.Play("StartHurt");
        MusicSystem.Instance.PlaySound(eSound.Hit);
        AddForce();
        Dragon.health--;
        GameEventManager.Instance.OnDinoHeart(Dragon.health);
        if (Dragon.health == 0)
        {
            GameEventManager.Instance.OnDinoDead();
        }
    }

    private void AddForce()
    {
        Dragon.Rigidbody2DThis.velocity = Vector2.zero;
        if (Dragon.FaceDirect == Vector2.right)
        {
            Dragon.Rigidbody2DThis.AddForce(new Vector2(-1, 1) * Dragon.jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            Dragon.Rigidbody2DThis.AddForce(new Vector2(1, 1) * Dragon.jumpForce, ForceMode2D.Impulse);
        }
    }

    public override void StateUpdate()
    {
        if (Dragon.CheckGround())
        {
            Dragon.Rigidbody2DThis.velocity = Vector2.zero;
            Dragon.SetState(eDragonState.Idle);
        }
    }

    public override void StateEnd() { }
}

public class DragonState_Dead : IDragonState
{
    public DragonState_Dead(Dragon dragon) : base(dragon)
    {
    }

    public override eDragonState State { get { return eDragonState.Dead; } }

    public override void StateStart()
    {
        MusicSystem.Instance.PlaySound(eSound.Death);
    }

    public override void StateUpdate() { }
    public override void StateEnd() { }
}