using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor",.2f);
    }
    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        if(Input.GetKeyUp(KeyCode.L))
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(Input.GetKeyDown(KeyCode.D)&&player.facingDir == -1)
        {
            player.Flip();
        }
        else if(Input.GetKeyDown(KeyCode.A)&&player.facingDir == 1)
        {
            player.Flip();
        }
    }
}
