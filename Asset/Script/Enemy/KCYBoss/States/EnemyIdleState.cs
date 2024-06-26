using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.movementSpeedModifier = 0f;
        base.Enter();
        //animation
    }

    public override void Exit()
    {
        base.Exit();
        //stopAnim
    }

    public override void Update()
    {
        base.Update();

        /*
        if(IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Idle]);
            return;
        }
        */
    }
}
