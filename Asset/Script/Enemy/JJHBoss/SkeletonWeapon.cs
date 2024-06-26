using UnityEngine;

public class SkeletonWeapon : MonoBehaviour
{
    private DashBoss enemy;

    private HealthSystem playerHealth;

    public void Initialize(DashBoss enemy)
    {
        this.enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(enemy.stat.CurrentStat.attackSO.target, collision.gameObject.layer))
        {
            playerHealth = collision.GetComponent<HealthSystem>();

            if (null != playerHealth)
                playerHealth.ChangeHealth(-enemy.stat.CurrentStat.attack);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}