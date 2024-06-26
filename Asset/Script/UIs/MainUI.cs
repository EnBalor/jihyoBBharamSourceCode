using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stageNum;
    [SerializeField]
    private TextMeshProUGUI goldAmount;
    [SerializeField]
    private TextMeshProUGUI attackTxt;
    [SerializeField]
    private TextMeshProUGUI defenseTxt;
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Slider stBar;

    private PlayerStatHandler playerStatHandler;
    private HealthSystem healthSystem;
    
    private void Start()
    {
        playerStatHandler = GameManager.Instance.Player.GetComponent<PlayerStatHandler>();
        healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
    }

    public void Update()
    {
        stageNum.text = GameManager.Instance.STAGE.ToString();
        goldAmount.text = GameManager.Instance.GOLD.ToString();
        attackTxt.text = playerStatHandler.CurrentStat.attack.ToString();
        if(playerStatHandler.CurrentStat is PlayerStat current)
        {
            defenseTxt.text = current.defense.ToString();
        }
        hpBar.value = healthSystem.CurrentHealth / healthSystem.MaxHealth;
    }
}
