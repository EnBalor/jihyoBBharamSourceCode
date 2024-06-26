using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Weapon w;
    public GameObject Weapons;
    
    [Header("Movement")]
    public float Speed;
    private Vector2 curMovementInput;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField]
    private SpriteRenderer weaponSprite;
    public Transform pos;

    [Header("Slide")]
    public float slideSpeed;
    public float slideDuration;
    public bool isSliding;
    private float slideTime;

    private static readonly int isRun = Animator.StringToHash("isRunning");
    private static readonly int isAttack = Animator.StringToHash("isAttack");
    private static readonly int isSlide = Animator.StringToHash("isSliding");

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.1f;
    private bool isShooting = false;

    private bool isWalk = false;
    private Coroutine footStepCoroutine;

    public Action OnInteraction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        w = Weapons.GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        if (!isSliding)
        {
            Move();
        }
        else
        {
            Slide();
        }
    }

    //private void Update()
    //{
    //    if (isShooting)
    //    {
    //        Fire();
    //    }
    //}

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            anim.SetBool(isRun, true);

            if(!isWalk)
            {
                isWalk = true;
                footStepCoroutine = StartCoroutine(FootStepSound());
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            anim.SetBool(isRun, false);

            isWalk = false;
            if(footStepCoroutine != null)
            {
                StopCoroutine(footStepCoroutine);
                footStepCoroutine = null;
            }
        }
    }

    public void OnSlideInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            StartSlide();
            anim.SetBool(isSlide, true);
            gameObject.layer = 8;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopSlide();
            anim.SetBool(isSlide, false);
            gameObject.layer = 6;
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            StartCoroutine("OnFire");
            anim.SetBool(isAttack, true);
            //isShooting = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopCoroutine("OnFire");
            anim.SetBool(isAttack, false);
            //isShooting = false;
        }
    }

    public void OnInterActionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnInteraction?.Invoke();
        }
    }

    //private void Fire()
    //{
    //    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //    Bullet bulletScript = bullet.GetComponent<Bullet>();
    //    bulletScript.speed *= sprite.flipX ? -1 : 1;
    //}

    IEnumerator OnFire()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(GetComponent<PlayerStatHandler>().CurrentStat.attackSO, GetComponent<PlayerStatHandler>().CurrentStat.attack);
            bulletScript.speed *= sprite.flipX ? -1 : 1;
            AudioManager.instance.PlaySFX("PlayerAttack");
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Move()
    {
        Vector2 dir = curMovementInput * Speed;
        rb.velocity = new Vector2(dir.x, rb.velocity.y);

        if (curMovementInput.x > 0)
        {
            sprite.flipX = false;
            weaponSprite.flipX = false;
            firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);
        }
        else if (curMovementInput.x < 0)
        {
            sprite.flipX = true;
            weaponSprite.flipX = true;
            firePoint.localPosition = new Vector3(-Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);
        }
    }

    private void Slide()
    {
        Debug.Log("슬라이드!");

        float elapsed = Time.time - slideTime;
        if (elapsed >= slideDuration)
        {
            isSliding = false;
            return;
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTime = Time.time;
    }

    private void StopSlide()
    {
        isSliding = false;
    }

    private IEnumerator FootStepSound()
    {
        while(isWalk)
        {
            AudioManager.instance.PlaySFX("Running");

            yield return new WaitForSeconds(0.3f);
        }
    }
}
