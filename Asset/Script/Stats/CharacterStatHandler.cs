using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterStatHandler : MonoBehaviour
{
    public CharacterStat CurrentStat { get; protected set; }

    protected List<CharacterStat> statModifiers = new List<CharacterStat>();

    protected Func<float, float, float> operation;

    public virtual void AddStatModifier(CharacterStat modifier)
    {
        statModifiers.Add(modifier);

        UpdateStat();
    }

    public virtual void RemoveStatModifier(CharacterStat modifier)
    {
        statModifiers.Remove(modifier);

        UpdateStat();
    }

    protected abstract void UpdateStat();

    protected virtual void ApplyStatModifier(CharacterStat modifier)
    {
        operation = modifier.statsChangeType switch
        {
            StatsChangeType.Add => (current, change) => current + change,
            StatsChangeType.Multiple => (current, change) => current * change,
            _ => (current, change) => change
        };
    }
}