using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if(player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        if(player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed *.8f * xInput, rb.velocity.y);
        }
        rb.velocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1) * Time.deltaTime;
    }
    public override void Exit()
    {
        base.Exit();
    }
}
