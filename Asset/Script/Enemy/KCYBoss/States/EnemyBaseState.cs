using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBaseState : CharacterState
{
    protected EnemyStateMachine stateMachine;
    //need data

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        // need data
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        
    }

    protected void StartAnim(int animatorHash)
    {
        stateMachine.enemy.animator.SetBool(animatorHash, true);
    }

    protected void StopAnim(int animatorHash)
    {
        stateMachine.enemy.animator.SetBool(animatorHash, false);
    }

    //protected bool IsInChaseRange()
    //{
    //    //float playerDistance = (stateMachine.player.transform.position - stateMachine.enemy.transform.position).sqrMagnitude;

    //    //return playerDistance <= stateMachine.enemy.data.playerChasingDistance * stateMachine.enemy.data.playerChasingDistance;
    //}
}
