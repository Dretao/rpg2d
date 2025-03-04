using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    override public void Enter()
    {
        base.Enter();
    }
    override public void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if(!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }
    }
    override public void Exit()
    {
        base.Exit();
    }

}
