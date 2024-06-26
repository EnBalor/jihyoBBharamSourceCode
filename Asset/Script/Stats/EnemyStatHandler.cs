using System;
using System.Data;
using System.Linq;
using System.Net;
using UnityEngine;

public class EnemyStatHandler : CharacterStatHandler
{
    [SerializeField] private EnemyStat baseStat;

    private readonly float minAttackDelay = 0.05f;
    private readonly float minAttackSize = 1f;
    private readonly float minAttackSpeed = 0.1f;

    private readonly float minSpeed = 0.5f;
    private readonly int minMaxHealth = 100;
    private readonly int minAttack = 1;
    private readonly int minDropMoney = 1000;

    private void Awake()
    {
        CurrentStat = new EnemyStat();

        UpdateStat();

        if (null != baseStat.attackSO)
            CurrentStat.attackSO = Instantiate(baseStat.attackSO);
    }

    public override void AddStatModifier(CharacterStat modifier)
    {
        base.AddStatModifier(modifier);
    }

    public override void RemoveStatModifier(CharacterStat modifier)
    {
        base.RemoveStatModifier(modifier);
    }

    protected override void ApplyStatModifier(CharacterStat modifier)
    {
        base.ApplyStatModifier(modifier);

        UpdateBaseStat(operation, modifier);
        UpdateAttackStat(operation, modifier);
    }

    private void UpdateBaseStat(Func<float, float, float> operation, CharacterStat modifier)
    {
        CurrentStat.maxHealth = Mathf.Max((int)operation(CurrentStat.maxHealth, modifier.maxHealth), minMaxHealth);
        CurrentStat.attack = Mathf.Max((int)operation(CurrentStat.attack, modifier.attack), minAttack);

        if ((CurrentStat is EnemyStat current) && (modifier is EnemyStat enemyStat))
        {
            current.speed = MathF.Max(operation(current.speed, enemyStat.speed), minSpeed);
            current.attackRange = operation(current.attackRange, enemyStat.attackRange);
            current.chaseRange = operation(current.chaseRange, enemyStat.chaseRange);
            current.dropMoney = (int)MathF.Max((int)operation(current.dropMoney, enemyStat.dropMoney), minDropMoney);
            current.enemyName = enemyStat.enemyName;
        }
    }

    private void UpdateAttackStat(Func<float, float, float> operation, CharacterStat modifier)
    {
        if ((null == CurrentStat.attackSO) || (null == modifier.attackSO))
            return;

        var currentAttack = CurrentStat.attackSO;
        var modAttack = modifier.attackSO;

        currentAttack.delay = Mathf.Max(operation(currentAttack.delay, modAttack.delay), minAttackDelay);
        currentAttack.size = Mathf.Max(operation(currentAttack.size, modAttack.size), minAttackSize);
        currentAttack.speed = Mathf.Max(operation(currentAttack.speed, modAttack.speed), minAttackSpeed);
    }

    protected override void UpdateStat()
    {
        ApplyStatModifier(baseStat);

        foreach (CharacterStat stat in statModifiers.OrderBy(o => o.statsChangeType))
            ApplyStatModifier(stat);
    }
}