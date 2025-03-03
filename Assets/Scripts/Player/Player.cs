using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }

    #endregion

    public bool isBusy { get; private set; } 
    [Header("Move info")]
    [SerializeField]public float moveSpeed = 8f;

    [Header("Jump info")]
    [SerializeField]public float jumpForce;
    [SerializeField]public float wallJumpForce = 10f;
    [SerializeField]public float lowJumpMultiplier;
    [SerializeField]public float fallMultiplier;

    

    [Header("Dash info")]
    [SerializeField] public float dashTime = 1.5f;
    [SerializeField] public float dashSpeed = 15f;
    [SerializeField] public float dashCoolDown;
    [SerializeField] public float dashUsageTimer;
    public float dashDir { get; private set; }

    [Header("attack details")]
    public Vector2[] attackMovement;



    

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        airState = new PlayerAirState(this, stateMachine, "jump");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "attack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
        dashUsageTimer -= Time.deltaTime;
    }

    private void CheckForDashInput()
    {
        if(isWallDetected())
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)&&dashUsageTimer < 0)
        {
            dashUsageTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);
        }
    }
    
    
    

    
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
    public IEnumerator BusyFor(float _scends)
    {
        isBusy = true;

        yield return new WaitForSeconds(_scends);

        isBusy = false;
    }
}
