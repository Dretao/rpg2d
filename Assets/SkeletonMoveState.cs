using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Enemy_Skeleton skeleton;
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        skeleton.SetVelocity(skeleton.moveSpeed * skeleton.facingDir, skeleton.rb.velocity.y);

        if(skeleton.isWallDetected() || !skeleton.IsGroundDetected())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
