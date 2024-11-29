using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public float speed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [HideInInspector] public bool isRunning = false;
    public AtkCollider atkCollider;
    public Vector3 direction;

    // State 
    public enum CharState
    { Normal, Run, Attack, Die }
    public CharState curState;
    //bool isRun;

    [Header("Attack")]
    public int noOfClicks = 0;
    float lastClickTime = 0;
    public float maxCombooDelay = 0.9f;
    public bool isAttacking = false;
    bool hasSubStamina = false;

    [Header("Status")]
    public Slider staminaSlider;
    [HideInInspector] public float maxStm = 100f;
    public float curStamina;
    
    [HideInInspector] public float countReturn;


    private void Awake()
    {
        maxStm = 100f;
        curStamina = maxStm;
        staminaSlider.value = curStamina;
    }

    public void ChageState(CharState newState)
    {
        switch (curState)
        {
            case CharState.Normal:
                break;
            case CharState.Run:
                break;
            case CharState.Attack:
                break;
            case CharState.Die:
                break;
        }

        switch (newState)
        {
            case CharState.Normal:
                animator.SetBool("Run", false);
                //isAttacking = false;
                speed = 3f;
                break;
            case CharState.Run:
                animator.SetBool("Run", true);
                speed = 7f;
                break;
            case CharState.Attack:
                //animator.SetBool("Attack", true);
                animator.SetFloat("Speed", 0f);
                break;
            case CharState.Die:
                break;
        }

        curState = newState;
    }

    private void FixedUpdate()
    {
        switch(curState)
        {
            case CharState.Normal:
                Movement();
                break;
            case CharState.Run:
                Movement();
                break;
        }
    }

    private void Update()
    {
        Attack();
        RecoveryStamina();
        SubStaminaWhenRun();
    }

    private void Movement()
{
    if (isAttacking) return;

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    direction = new Vector3(horizontal, 0f, vertical).normalized;

    if (direction.magnitude >= 0.1f)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);

        if (Input.GetMouseButton(1) && curState != CharState.Run && curStamina > 3f)
        {
            ChageState(CharState.Run);
            isRunning = true;
        }
        else if (curState == CharState.Run && (curStamina <= 3f || !Input.GetMouseButton(1)))
        {
            // Chuyển về Normal khi hết Stamina hoặc không giữ chuột phải
            ChageState(CharState.Normal);
            isRunning = false;
        }
    }
    else if (curState != CharState.Normal)
    {
        ChageState(CharState.Normal);
    }

    animator.SetFloat("Speed", direction.magnitude);
}

        #region Code Attack
    void Attack()
    {
        if (noOfClicks > 0)
        {
            isAttacking = true;
        }
        else if (noOfClicks == 0)
        {
            isAttacking = false;
        }

        if (Time.time - lastClickTime > maxCombooDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetMouseButtonDown(0) && curStamina >= 7f) // Chỉ cho phép Attack nếu curStamina đủ
        {
            lastClickTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                SubStaminaWhenAttack();
                animator.SetBool("Attack01", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 5);
        }
    }

    public void EndAttack01()
    {
        if (noOfClicks >= 2)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack02", true);
            animator.SetBool("Attack01", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack01", false);
        }
    }

    public void EndAttack02()
    {
        if (noOfClicks >= 3)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack03", true);
            animator.SetBool("Attack02", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack02", false);
        }
    }

    public void EndAttack03()
    {
        if (noOfClicks >= 4)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack04", true);
            animator.SetBool("Attack03", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack03", false);
        }
    }

    public void EndAttack04()
    {
        if (noOfClicks >= 5)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack05", true);
            animator.SetBool("Attack04", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack04", false);
        }
    }

    public void EndAttack05()
    {
        isAttacking = false;
        animator.SetBool("Attack01", false);
        animator.SetBool("Attack02", false);
        animator.SetBool("Attack03", false);
        animator.SetBool("Attack04", false);
        animator.SetBool("Attack05", false);
        noOfClicks = 0;
    }
    #endregion

    #region Stamina
    public void SubStaminaWhenRun()
    {
        if (curState == CharState.Run)
        {
            curStamina -= 3f * Time.deltaTime;
            staminaSlider.value = curStamina;

            if (curStamina <= 3) // Khi stamina về 0, chuyển sang trạng thái Normal
            {
                curStamina = 0;
                ChageState(CharState.Normal);
                isRunning = false;
            }
        }
    }

    public void SubStaminaWhenAttack()
    {
        curStamina -= 7f; // Giảm stamina khi Attack
        if (curStamina < 0)
        {
            curStamina = 0;
        }
        staminaSlider.value = curStamina;
    }

    public void RecoveryStamina()
    {
        if (curState == CharState.Run || isAttacking) return;
        if (curStamina < 3f && Input.GetMouseButton(1)) return;
        if (curStamina < maxStm)
        {
            curStamina += countReturn * Time.deltaTime;
            staminaSlider.value = curStamina;
        }

        if (direction.magnitude <= 0f)
        {
            countReturn = 5f;
        }
        else if (curState == CharState.Normal)
        {
            countReturn = 3f;
        }
    }
    #endregion
}
