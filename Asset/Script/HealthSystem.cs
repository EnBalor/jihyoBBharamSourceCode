using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private CharacterStatHandler stat;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibillityEnd;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        stat = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        MaxHealth = stat.CurrentStat.maxHealth;
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (isAttacked && (timeSinceLastChange < healthChangeDelay))
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibillityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)
            return false;

        timeSinceLastChange = 0f;

        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

        if (0f >= CurrentHealth)
        {
            OnDeath?.Invoke();
            return true;
        }

        if (0f <= change)
            OnHeal?.Invoke();
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true;
    }
}