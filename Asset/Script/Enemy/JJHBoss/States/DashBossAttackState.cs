using UnityEngine;

public class DashBossAttackState : EnemyBaseState
{
    float playerDistance;

    public DashBossAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.movementSpeedModifier = 0f;

        if (stateMachine.enemy is DashBoss dash)
            StartAnim(dash.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        if (stateMachine.enemy is DashBoss dash)
            StopAnim(dash.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.enemy.stat.CurrentStat is EnemyStat enemyStat)
        {
            if (1f <= GetNormalizedTime(stateMachine.enemy.animator, "Attack"))
            {
                if (!CheckTargetInRange(enemyStat.chaseRange))
                {
                    stateMachine.ChangeState(stateMachine.StateDict[(int)DashBossState.Idle]);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(stateMachine.StateDict[(int)DashBossState.Chase]);
                    return;
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private bool CheckTargetInRange(float range)
    {
        playerDistance = (stateMachine.enemy.transform.position - stateMachine.player.transform.position).sqrMagnitude;

        return playerDistance <= (range * range);
    }

    private float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
            return nextInfo.normalizedTime;
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
            return currentInfo.normalizedTime;
        else
            return 0f;
    }
}