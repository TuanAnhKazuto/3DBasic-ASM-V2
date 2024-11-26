using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;

    public float speed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

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

        switch(newState)
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

        curState = newState;
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float tarrgetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tarrgetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, tarrgetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, tarrgetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
