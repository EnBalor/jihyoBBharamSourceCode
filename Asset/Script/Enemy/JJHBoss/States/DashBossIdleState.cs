using Unity.VisualScripting;

public class DashBossIdleState : EnemyBaseState
{
    float playerDistance = 0f;

    public DashBossIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.movementSpeedModifier = 0f;

        if (stateMachine.enemy is DashBoss dash)
        {
            StartAnim(dash.AnimationData.GroundParameterHash);
            StartAnim(dash.AnimationData.IdleParameterHash);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (stateMachine.enemy is DashBoss dash)
        {
            StopAnim(dash.AnimationData.IdleParameterHash);
            StopAnim(dash.AnimationData.GroundParameterHash);
        }
    }

    public override void Update()
    {
        base.Update();

        if (CheckTargetNear())
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)DashBossState.Chase]);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private bool CheckTargetNear()
    {
        if (stateMachine.enemy.stat.CurrentStat is EnemyStat curStat)
        {
            playerDistance = (stateMachine.enemy.transform.position - stateMachine.player.transform.position).sqrMagnitude;

            return playerDistance <= (curStat.chaseRange * curStat.chaseRange); 
        }

        return false;
    }
}
