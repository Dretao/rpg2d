using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton skeleton;

    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if(skeleton.IsPlayerDetected())
        {
            stateTimer = skeleton.battleTime;
            if(skeleton.IsPlayerDetected().distance < skeleton.attackDistance && CanAttack())
            {
                stateMachine.ChangeState(skeleton.attackState);
            }
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position, skeleton.transform.position) > skeleton.detectDistance)
            {
                stateMachine.ChangeState(skeleton.idleState);
            }
        }
        if(player.position.x > skeleton.transform.position.x)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }

        skeleton.SetVelocity(skeleton.moveSpeed * moveDir, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if(Time.time >= skeleton.lastTimeAttacked + skeleton.attackCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
