using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatHandler : CharacterStatHandler
{
    [SerializeField] private PlayerStat baseStat;

    public Dictionary<UpgradeStatType, int> statDict;    

    private readonly float minAttackDelay = 0.05f;
    private readonly float minAttackSize = 1f;
    private readonly float minAttackSpeed = 0.1f;

    private readonly int minMaxHealth = 100;
    private readonly int minAttack = 1;
    private readonly float minDefense = 5f;
    private readonly float minStemina = 10f;

    private void Awake()
    {
        CurrentStat = new PlayerStat();

        statDict = new Dictionary<UpgradeStatType, int>();

        for(int i = 0; i < (int)UpgradeStatType.COUNTEND; i++)
        {
            statDict.Add((UpgradeStatType)i, 1);
        }

        UpdateStat();

        if (null != baseStat.attackSO)
        {
            baseStat.attackSO = Instantiate(baseStat.attackSO);
            CurrentStat.attackSO = Instantiate(baseStat.attackSO);
        }
    }

    public override void AddStatModifier(CharacterStat modifier)
    {
        base.AddStatModifier(modifier);

        if (modifier is PlayerStat playerStat)
            ++statDict[playerStat.upgradeStatType];
    }

    public override void RemoveStatModifier(CharacterStat modifier)
    {
        base.RemoveStatModifier(modifier);

        if (modifier is PlayerStat playerStat)
            --statDict[playerStat.upgradeStatType];
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

        if ((CurrentStat is PlayerStat current) && (modifier is PlayerStat modified))
        {
            current.defense = MathF.Max(operation(current.defense, modified.defense), minDefense);
            current.stamina = MathF.Max(operation(current.stamina, modified.stamina), minStemina);
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
