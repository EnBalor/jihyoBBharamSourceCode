using UnityEngine;

public class DashBoss : Enemy
{
    [field: Header("Animation")]
    [field: SerializeField] public DashBossAnimationData AnimationData { get; private set; }

    [SerializeField] private SkeletonWeapon skeletonWeapon;

    protected override void Awake()
    {
        base.Awake();

        AnimationData.Initialize();
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.StateDict.Add((int)DashBossState.Idle, new DashBossIdleState(stateMachine));
        stateMachine.StateDict.Add((int)DashBossState.Chase, new DashBossChasingState(stateMachine));
        stateMachine.StateDict.Add((int)DashBossState.Attack, new DashBossAttackState(stateMachine));
        //stateMachine.StateDict.Add((int)DashBossState.Move, new EnemyMoveState(stateMachine));

        stateMachine.ChangeState(stateMachine.StateDict[(int)DashBossState.Idle]);

        skeletonWeapon.Initialize(this);
    }

    private void OnAttackEvent()
    {
        skeletonWeapon.gameObject.SetActive(true);
    }

    private void OffAttackEvent()
    {
        skeletonWeapon.gameObject.SetActive(false);
    }
}