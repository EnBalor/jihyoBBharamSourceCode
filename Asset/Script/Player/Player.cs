using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerController controller;

    public HealthSystem healthSystem { get; private set; }

    private void Awake()
    {
        if (null == GameManager.Instance.Player)
        {
            GameManager.Instance.Player = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        controller = GetComponent<PlayerController>();
        healthSystem = GetComponent<HealthSystem>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (null != healthSystem)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibillityEnd += InvincibillityEnd;
        }
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
    }

    private void InvincibillityEnd()
    {
    }
}
