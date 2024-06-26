using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 60f;
    public float far = 10f;
    private Rigidbody2D rb;
    private Vector2 startPosition;

    private HealthSystem enemyHealth;
    private AttackSO attackData;
    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    private void Update()
    {
        float distance = Vector2.Distance(startPosition, rb.position);
        if (distance > far)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    public void Initialize(AttackSO attackSO, float damage)
    {
        attackData = attackSO;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(attackData.target, collision.gameObject.layer))
        {
            enemyHealth = collision.GetComponent<HealthSystem>();

            if (null != enemyHealth)
                enemyHealth.ChangeHealth(-damage);

            // 충돌 시 총알 제거
            Destroy(gameObject);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}
