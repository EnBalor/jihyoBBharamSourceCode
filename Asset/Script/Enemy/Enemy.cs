using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator { get; private set; }

    protected EnemyStateMachine stateMachine;

    public EnemyStatHandler stat { get; private set; }
    public HealthSystem healthSystem { get; private set; }
    
    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rigidbody { get; private set; }

    [SerializeField] private GameObject bossUI;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpTxt;
    [SerializeField] private TextMeshProUGUI nameTxt;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stat = GetComponent<EnemyStatHandler>();
        healthSystem = GetComponent<HealthSystem>();

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        stateMachine = new EnemyStateMachine(this);

        if (null != healthSystem)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibillityEnd += InvincibillityEnd;
            healthSystem.OnDeath += Death;
        }

        bossUI.SetActive(true);
        if (stat.CurrentStat is EnemyStat enemyStat)
            nameTxt.text = enemyStat.enemyName;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();

        if (bossUI.activeInHierarchy)
        {
            hpBar.value = healthSystem.CurrentHealth / healthSystem.MaxHealth;
            hpTxt.text = ((healthSystem.CurrentHealth / healthSystem.MaxHealth) * 100f).ToString();
        }
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    
    private void Hit()
    {
        animator.SetTrigger("Hit");
    }

    private void InvincibillityEnd()
    {
    }

    private void Death()
    {
        animator.SetTrigger("Dead");

        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;

        if (stat.CurrentStat is EnemyStat enemyStat)
            GameManager.Instance.GOLD += enemyStat.dropMoney;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;

            color = Color.gray;
            color.a = 0.9f;
            renderer.color = color;
        }

        Destroy(gameObject, 2f);
    }
}
