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
        healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>(); // �̱������� ������ ��쿣 �̷��� ���

        if(healthSystem.MaxHealth == healthSystem.CurrentHealth)
        {
            Debug.Log("ȸ�� ���ʿ�");
        }
        else
        {
            if(GameManager.Instance.GOLD < healPrice)
            {
                Debug.Log("���� ���ڶ�");
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
