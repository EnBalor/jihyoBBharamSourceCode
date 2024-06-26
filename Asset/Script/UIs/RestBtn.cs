using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestBtn : MonoBehaviour
{
    HealthSystem healthSystem;

    float healValue;
    [SerializeField] int healPrice;

    public void RestHpFill()
    {
        healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>(); // 싱글톤으로 만들어둔 경우엔 이렇게 사용

        if(healthSystem.MaxHealth == healthSystem.CurrentHealth)
        {
            Debug.Log("회복 불필요");
        }
        else
        {
            if(GameManager.Instance.GOLD < healPrice)
            {
                Debug.Log("돈이 모자라");
            }
            else
            {
                healValue = healthSystem.MaxHealth;
                healthSystem.ChangeHealth(healValue);
                Debug.Log(healValue);

                GameManager.Instance.UseGold(healPrice);
                AudioManager.instance.PlaySFX("Heal");
            }
        }
    }
}
