using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }


    public override void Enter()
    {
        stateMachine.movementSpeedModifier = 1f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        /*
        if(!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Idle]);
            return;
        }

        else if(!InAttackrange())
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Attack]);
            return;
        }
        */

        //JJH Working
        //stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Move]);

        //stateMachine.ChangeState(stateMachine.moveState);
    }

    //protected bool InAttackrange()
    //{
    //    float playerDistance = (stateMachine.player.transform.position - stateMachine.enemy.transform.position).sqrMagnitude;
    //    return playerDistance <= stateMachine.enemy.data.attackRange * stateMachine.enemy.data.attackRange;
    //}
}
