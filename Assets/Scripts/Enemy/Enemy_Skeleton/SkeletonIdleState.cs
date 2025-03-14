using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    Enemy_Skeleton skeleton;

    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName, _skeleton)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = skeleton.idleTime;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if(stateTimer <= 0)
        {
            stateMachine.ChangeState(skeleton.moveState);
        }

        
    }
}
