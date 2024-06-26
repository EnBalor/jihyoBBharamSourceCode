using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeam : MonoBehaviour
{
    public float speed;
    public float duration;

    public HealthSystem healthSystem;
    public EnemyStatHandler enemyStat;
    public AttackSO attackData;

    public GameObject player;
    public GameObject enemy;

    private void Awake()
    {
        healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        enemy = FindObjectOfType<EnemyStatHandler>()?.gameObject;
        enemyStat = enemy.GetComponent<EnemyStatHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-enemyStat.CurrentStat.attack);
                AudioManager.instance.PlaySFX("PlayerHit");
            }
        }
    }

    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
