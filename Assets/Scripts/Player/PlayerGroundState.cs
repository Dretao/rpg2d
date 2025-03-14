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
        if(Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }
        if(Input.GetKeyDown(KeyCode.L) && HasNoSword())
        {
            stateMachine.ChangeState(player.aimSwordState);
        }
    }
    private bool HasNoSword()
    {
        if(!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
    override public void Exit()
    {
        base.Exit();
    }

}
