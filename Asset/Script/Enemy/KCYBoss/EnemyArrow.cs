using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{

    public float speed;
    public float duration;

    public HealthSystem healthSystem;
    public EnemyStatHandler enemyStat;
    public AttackSO attackData;

    public GameObject player;
    public GameObject enemy;
    private Rigidbody2D rigidbody;
    private Vector2 dir;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemy = FindObjectOfType<EnemyStatHandler>()?.gameObject;
        enemyStat = enemy.GetComponent<EnemyStatHandler>();
        player = GameManager.Instance.Player.gameObject;
    }
    private void Start()
    {
        dir = GetDir();

        if(dir.x <= 0)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        rigidbody.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-enemyStat.CurrentStat.attack);
                AudioManager.instance.PlaySFX("PlayerHit");
                Destroy(gameObject);
            }
        }
    }

    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    private Vector2 GetDir()
    {
        Vector2 dir = (player.transform.position - enemy.transform.position).normalized;
        return dir;
    }
}
