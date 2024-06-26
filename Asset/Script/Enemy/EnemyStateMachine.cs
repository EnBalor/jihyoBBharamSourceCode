using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy enemy { get; }
    
    public float movementSpeed { get; private set; }
    public float movementSpeedModifier { get; set; }

    public Player player { get; private set; }
    
    //JJH Working
    public Dictionary<int, EnemyBaseState> StateDict { get; set; }
    
    public EnemyStateMachine(Enemy enemy)
    {
        this.enemy = enemy;

        player = GameManager.Instance.Player;

        StateDict = new Dictionary<int, EnemyBaseState>();

        if (enemy.stat.CurrentStat is EnemyStat enemyStat)
            movementSpeed = enemyStat.speed;
        else
            movementSpeed = 0f;
    }
}
