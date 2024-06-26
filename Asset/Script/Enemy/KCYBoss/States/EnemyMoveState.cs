using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    private RangeBoss rangeBoss;
    private EnemyStatHandler statHandler;

    public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        rangeBoss = stateMachine.enemy as RangeBoss;
    }

    public override void Enter()
    {
        //JJH Working
        stateMachine.movementSpeedModifier = 1f;
        stateMachine.enemy.animator.SetBool("isMove", true);
        base.Enter();
    }

    public override void Exit()
    {
        stateMachine.enemy.animator.SetBool("isMove", false);
        base.Exit();
    }

    public override void HandleInput()
    {
    }

    public override void PhysicsUpdate()
    {
    }

    public override void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 dir = GetDir();

        if (dir.x <= 0)
        {
            rangeBoss?.FlipEffectPosition(true);
            stateMachine.enemy.spriteRenderer.flipX = true;
        }

        else
        {
            rangeBoss?.FlipEffectPosition(false);
            stateMachine.enemy.spriteRenderer.flipX = false;
        }

        Move(dir);
    }

    private Vector2 GetDir()
    {
        Vector2 dir = (stateMachine.player.transform.position - stateMachine.enemy.transform.position).normalized;
        return dir;
    }

    private void Move(Vector2 dir)
    {
        float speed = GetSpeed();

        Vector2 currentVelocity = stateMachine.enemy.rigidbody.velocity;

        stateMachine.enemy.rigidbody.velocity = new Vector2(dir.x * speed, currentVelocity.y);
    }

    private float GetSpeed()
    {
        float speed = stateMachine.movementSpeed * stateMachine.movementSpeedModifier;
        return speed;
    }
}
