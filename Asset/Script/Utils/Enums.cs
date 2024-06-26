public enum BossState
{
    Idle,
    Move,
    Chase,
    Attack,
    RangeAttack
}

public enum DashBossState
{
    Idle,
    Chase,
    Attack,
    DashAttack,
    Chop,
    Dead
}

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

public enum UpgradeStatType
{
    MaxHealth,
    Attack,
    Defense,
    Stamina,
    COUNTEND
}
