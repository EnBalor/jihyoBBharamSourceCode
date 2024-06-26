using UnityEngine;

public class DashBossChasingState : EnemyBaseState
{
    float playerDistance;

    public DashBossChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.movementSpeedModifier = 1f;

        if (stateMachine.enemy is DashBoss dash)
        {
            StartAnim(dash.AnimationData.GroundParameterHash);
            StartAnim(dash.AnimationData.ChaseParameterHash);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (stateMachine.enemy is DashBoss dash)
        {
            StopAnim(dash.AnimationData.ChaseParameterHash);
            StopAnim(dash.AnimationData.GroundParameterHash);
        }
    }

    public override void Update()
    {
        base.Update();

        Movement();

        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.StateDict[(int)DashBossState.Attack]);
            return;
        }
    }

    private void Movement()
    {
        Vector2 dir = GetDir();

        if (dir.x <= 0)
            stateMachine.enemy.transform.right = Vector2.left;
        else
            stateMachine.enemy.transform.right = Vector2.right;

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

        float currentVelocityY = stateMachine.enemy.rigidbody.velocity.y;

        stateMachine.enemy.rigidbody.velocity = new Vector2(dir.x * speed, currentVelocityY);
    }

    private float GetSpeed()
    {
        float speed = stateMachine.movementSpeed * stateMachine.movementSpeedModifier;

        return speed;
    }

    private bool IsInAttackRange()
    {
        if (stateMachine.enemy.stat.CurrentStat is EnemyStat curStat)
        {
            playerDistance = (stateMachine.enemy.transform.position - stateMachine.player.transform.position).sqrMagnitude;

            return playerDistance <= (curStat.attackRange * curStat.attackRange);
        }

        return false;
    }
}
