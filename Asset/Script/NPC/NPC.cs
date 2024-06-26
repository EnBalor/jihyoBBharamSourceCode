using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public enum NPCType
{
    StatUpgrade,
    Rest
}
public class NPC : MonoBehaviour
{
    public NPCType npcType;

    public GameObject inputKey;
    public GameObject shopUI;

    [SerializeField]
    private bool isOpen = false;

    private void Start()
    {
        GameManager.Instance.Player.controller.OnInteraction += InterAction;
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.controller.OnInteraction -= InterAction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            inputKey.SetActive(true);
            isOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {   
            inputKey.SetActive(false);
            isOpen = false;
            shopUI.SetActive(false);
        }
    }

    public void InterAction()
    {
        if(isOpen)
        {
            shopUI.SetActive(true);
        }
    }
}
