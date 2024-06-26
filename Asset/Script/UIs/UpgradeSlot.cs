using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI statName;
    [SerializeField]
    private TextMeshProUGUI currentLv;
    [SerializeField]
    private TextMeshProUGUI NextLv;
    [SerializeField]
    private TextMeshProUGUI requireGoldTxt;
    [SerializeField]
    private Button upgrade;

    private int requireGold = 100;

    private PlayerStatHandler playerStatHandler;
    [SerializeField]
    private PlayerStat playerStat;
    private UpgradeStatType upgradeStatType;

    private void Update()
    {
        if (GameManager.Instance.GOLD >= requireGold)
        {
            upgrade.enabled = true;
        }
        else
        {
            upgrade.enabled = false;
        }
    }
    public void InitSlot(int index)
    {
        playerStatHandler = GameManager.Instance.Player.GetComponent<PlayerStatHandler>();
        icon.sprite = Resources.Load<Sprite>("ShopIcon" + index);
        upgradeStatType = (UpgradeStatType)index;
        statName.text = upgradeStatType.ToString();
        currentLv.text = playerStatHandler.statDict[upgradeStatType].ToString();
        NextLv.text = (playerStatHandler.statDict[upgradeStatType] + 1).ToString();
        requireGoldTxt.text = requireGold.ToString();

        playerStat.statsChangeType = StatsChangeType.Add;
        playerStat.attackSO = Instantiate(playerStat.attackSO);
        playerStat.upgradeStatType = upgradeStatType;

        switch (upgradeStatType)
        {
            case UpgradeStatType.MaxHealth:
                playerStat.maxHealth += 10;
                break;
            case UpgradeStatType.Attack:
                playerStat.attack += 1;
                break;
            case UpgradeStatType.Defense:
                playerStat.defense += 1;
                break;
            case UpgradeStatType.Stamina:
                playerStat.stamina += 10f;
                break;
        }
    }

    private void UpdateSlot()
    {
        currentLv.text = playerStatHandler.statDict[upgradeStatType].ToString();
        NextLv.text = (playerStatHandler.statDict[upgradeStatType] + 1).ToString();
        requireGoldTxt.text = requireGold.ToString();
    }

    public void Upgrade()
    {
        playerStatHandler.AddStatModifier(playerStat);
        GameManager.Instance.UseGold(requireGold);
        requireGold += 100;
        AudioManager.instance.PlaySFX("Upgrade");
        UpdateSlot();
    }
}
