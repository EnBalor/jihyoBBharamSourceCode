using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.movementSpeedModifier = 0f;
        base.Enter();
        stateMachine.enemy.animator.SetBool("isAttack", true);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.enemy.animator.SetBool("isAttack", false);
    }

    public override void Update()
    {
        /*
        base.Update();
        if(IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Chase]);
            return;
        }

        else
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Idle]);
            return;
        }*/
        
    }
}
