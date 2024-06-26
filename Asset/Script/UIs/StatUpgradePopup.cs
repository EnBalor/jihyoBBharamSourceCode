using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradePopup : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;

    private List<UpgradeSlot> upgradeSlotList = new List<UpgradeSlot>();
    private UpgradeSlot upgradeSlot;
    private GameObject obj;

    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            obj = Instantiate(slotPrefab,transform);
            upgradeSlot = obj.GetComponent<UpgradeSlot>();
            upgradeSlot.InitSlot(i);
            upgradeSlotList.Add(upgradeSlot);
        }
    }
}
