using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;

    public float speed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Attack
    bool isAttack = false;

    // State 
    [HideInInspector] public enum CharState
    { Normal, Run, Attack, Die}
    public CharState curState;

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
                animator.SetBool("Attack", false);
                isAttack = false;
                speed = 3f;
                break;
            case CharState.Run:
                animator.SetBool("Run", true);
                speed = 7f;
                break;
            case CharState.Attack:
                animator.SetBool("Attack", true);
                animator.SetFloat("Speed", 0f);
                isAttack = true;    
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
    }

    private void Movement()
    {
        if (isAttack) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            

            // Chỉ chuyển sang trạng thái Run nếu đang không phải Run
            if (Input.GetKey(KeyCode.LeftShift) && curState != CharState.Run)
            {
                ChageState(CharState.Run);
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && curState != CharState.Normal)
            {
                ChageState(CharState.Normal);
            }
        }
        else if (curState != CharState.Normal) // Đứng yên -> Chuyển về trạng thái Normal
        {
            ChageState(CharState.Normal);
        }

        animator.SetFloat("Speed", direction.magnitude);
    }


    void Attack()
    {
        if (Input.GetMouseButton(0) && curState != CharState.Attack)
        {
            ChageState(CharState.Attack);
        }
        else if (!Input.GetMouseButton(0) && curState == CharState.Attack)
        {
            ChageState(CharState.Normal);
        }
    }

}
