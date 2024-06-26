using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy",menuName = "Character/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int currentHealth;
    public int maxHealth;
    public float playerChasingDistance;
    public float attackRange;
    public float meleeAttackRange;
    public int damage;
    public float speed;
}
