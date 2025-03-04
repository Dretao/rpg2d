using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    Enemy_Skeleton skeleton;
    protected Transform player;

    public SkeletonGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
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
        if (skeleton.IsPlayerDetected() || Vector2.Distance(skeleton.transform.position, player.position) < 2)
        {
            stateMachine.ChangeState(skeleton.battleState);
        }
    }
}
