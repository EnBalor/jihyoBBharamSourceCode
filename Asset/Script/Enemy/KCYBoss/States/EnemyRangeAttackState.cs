using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeState : EnemyBaseState
{
    public EnemyRangeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.movementSpeedModifier = 0f;
        base.Enter();
        stateMachine.enemy.animator.SetBool("isRangeAttack", true);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.animator.SetBool("isRangeAttack", false);
    }

    public override void Update()
    {
        base.Update();
    }
}
