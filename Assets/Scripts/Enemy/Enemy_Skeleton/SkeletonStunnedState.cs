using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    Enemy_Skeleton skeleton;
    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = _skeleton;
    }
    public override void Enter()
    {
        base.Enter();
        skeleton.fX.InvokeRepeating("RedColorBlink",0,.1f);
        stateTimer = skeleton.stunDuration;
        rb.velocity = new Vector2(-skeleton.facingDir * skeleton.stunDirection.x, skeleton.stunDirection.y);
    }
    public override void Exit()
    {
        base.Exit();
        skeleton.fX.Invoke("CancelRedBlink",0);
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
