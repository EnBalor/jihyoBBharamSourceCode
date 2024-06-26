using UnityEngine;
using System.Collections;


public class RangeBoss : Enemy
{
    public Transform effectPosition;
    public GameObject meleeAttackEffect;
    public GameObject rangeAttackArrow;

    public float moveSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.StateDict.Add((int)BossState.Idle, new EnemyIdleState(stateMachine));
        stateMachine.StateDict.Add((int)BossState.RangeAttack, new EnemyRangeState(stateMachine));
        stateMachine.StateDict.Add((int)BossState.Attack, new EnemyAttackState(stateMachine));
        stateMachine.StateDict.Add((int)BossState.Move, new EnemyMoveState(stateMachine));

        StartCoroutine(EnemyPhase());

        //stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Idle]);
    }

    IEnumerator EnemyPhase()
    {
        while (true)
        {
            EnemyIdlePhase();

            yield return new WaitForSeconds(1f);

            EnemyChasingPhase();

            yield return new WaitForSeconds(1f);

            EnemyMeleeAttackPhase();

            yield return new WaitForSeconds(2f);

            EnemyIdlePhase();

            yield return new WaitForSeconds(1f);

            EnemyChasingPhase();

            yield return new WaitForSeconds(1f);

            EnemyRangeAttackPhase();

            yield return new WaitForSeconds(1f);
        }
    }

    private void EnemyRangeAttackPhase()
    {
        stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.RangeAttack]);
    }

    private void EnemyChasingPhase()
    {
        stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Move]);
    }

    private void EnemyMeleeAttackPhase()
    {
        stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Attack]);
    }

    private void EnemyIdlePhase()
    {
        stateMachine.ChangeState(stateMachine.StateDict[(int)BossState.Idle]);
    }

    public void FlipEffectPosition(bool flip)
    {
        Vector2 position = effectPosition.localPosition;
        position.x = flip ? -Mathf.Abs(position.x) : Mathf.Abs(position.x);
        effectPosition.localPosition = position;
    }

    public void CreateEffect()
    {
        GameObject effect = Instantiate(meleeAttackEffect, effectPosition.position, Quaternion.identity);
        Vector2 effectScale = effect.transform.localScale;

        if(spriteRenderer.flipX)
        {
            effectScale.x = -Mathf.Abs(effectScale.x);
        }

        else
        {
            effectScale.x = Mathf.Abs(effectScale.x);
        }

        meleeAttackEffect.transform.localScale = effectScale;

        Destroy(effect, 0.3f);
    }

    public void CreateProjectile()
    {
        GameObject effect = Instantiate(rangeAttackArrow, effectPosition.position, Quaternion.identity);
        Vector2 effectScale = effect.transform.localScale;

        if (spriteRenderer.flipX)
        {
            effectScale.x = -Mathf.Abs(effectScale.x);
        }

        else
        {
            effectScale.x = Mathf.Abs(effectScale.x);
        }

        meleeAttackEffect.transform.localScale = effectScale;

        Destroy(effect, 5f);
    }
}