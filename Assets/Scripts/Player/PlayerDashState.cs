using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateClone(player.transform);

        stateTimer = player.dashTime;
    }

    public override void Update()
    {
        base.Update();
        if(!player.IsGroundDetected()&&player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        rb.velocity = new Vector2(player.dashSpeed * player.dashDir, 0);
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0,rb.velocity.y);
    }
}
