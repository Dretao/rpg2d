using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);       
    }
    public override void Update()
    {
        base.Update();
        if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if(rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
