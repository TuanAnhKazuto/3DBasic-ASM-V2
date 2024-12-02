using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;
    public float maxHP;
    public float currentHP;
    public Animator animator;

    // state machine
    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }

    public CharacterState curState; // trạng thái hiện tại

    private void Start()
    {
        originalPos = transform.position;
        navMeshAgent.SetDestination(target.position);
    }

    private void Update()
    {
        // Khoảng cách từ vị trí hiện tại đến vị trí ban đầu
        var distanceToOriginal = Vector3.Distance(originalPos, transform.position);
        // Khoảng cách từ vị trí hiện tại đến vị trí mục tiêu

        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && distanceToOriginal <= maxDistace)
        {
            // di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
            distance = Vector3.Distance(target.position, transform.position);
            if (distance < 2f)
            {
                // Tấn công
                ChangeState(CharacterState.Attack);
            }
        }

        if (distance > radius || distanceToOriginal > maxDistace)
        {
            //quay ve vi tri ban dau
            navMeshAgent.SetDestination(originalPos);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            //chuyen sang trang thai dung yen
            distance = Vector3.Distance(originalPos, transform.position);
            if (distance < 1f)
            {
                animator.SetFloat("Speed", 0);
            }
            //bth
            ChangeState(CharacterState.Normal);
        }
    }

    private void ChangeState(CharacterState newState)
    {

        // B1: exit curState
        switch (curState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
        }

        // B2: enter newState
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
            case CharacterState.Die:
                animator.SetTrigger("Die");
                break;
        }

        // B3: Update state
        curState = newState;
    }

    public void TakeDamege(float damege)
    {
        currentHP -= damege;
        currentHP = Mathf.Max(0, currentHP);
        if (currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
        }
    }
}