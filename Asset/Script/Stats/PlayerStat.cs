using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat : CharacterStat
{
    public UpgradeStatType upgradeStatType;

    [Range(0, 100)] public float defense;
    public float stamina;
}
