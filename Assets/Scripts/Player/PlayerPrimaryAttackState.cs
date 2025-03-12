using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCount = 0;
    private float lastAttacked;
    private float comboWindow = 2;//允许连续攻击的时间窗口
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        xInput = 0;//reset xinput
        if(comboCount > 2  || Time.time - lastAttacked > comboWindow)
        {
            comboCount = 0;
            player.attackCheckRadius = 1.05f;
        }
        player.anim.SetInteger("ComboCount", comboCount);

        #region Choose attack direction

        float attackDir = player.facingDir;

        if(xInput != 0)
        {
            attackDir = xInput;
        }
        #endregion
        
        player.SetVelocity(player.attackMovement[comboCount].x * attackDir,player.attackMovement[comboCount].y);//attack movement
        stateTimer = .1f;
    }
    public override void Update()
    {
        base.Update();
        
        if(stateTimer<0)
            player.SetZeroVelocity();
        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(comboCount == 0)
        {
            player.attackCheckRadius = 1.05f;
        }
        else if(comboCount == 1)
        {
            player.attackCheckRadius = 0.8f;
        }
        else if(comboCount == 2)
        {
            player.attackCheckRadius = 1.42f;
        }
    }
    public override void Exit()
    {
        base.Exit();
        comboCount++;
        lastAttacked = Time.time;
        player.StartCoroutine("BusyFor", .15f);//busy for .15 seconds
    }
}
