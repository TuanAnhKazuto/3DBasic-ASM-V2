using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target; // mục tiêu
    
    public float radius = 10f; // bán kính tìm kiếm mục tiêu
    public Vector3 originalePosition; // vị trí ban đầu
    public float maxDistance = 50f; // khoảng cách tối đa
    
    
    public Animator animator; // khai báo component

    // state machine
    public enum CharacterState
    {
        Normal,
        Attack
    }
    public CharacterState currentState; // trạng thái hiện tại
    
    
    void Start()
    {
        originalePosition = transform.position;
        navMeshAgent.SetDestination(target.position);
    }
    
    void Update()
    {
        // khoảng cách từ vị trí hiện tại đến vị trí ban đầu
        var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        // khoảng cách từ vị trí hiện tại đến mục tiêu
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && distanceToOriginal <= maxDistance)
        {
            // di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
            
            distance = Vector3.Distance(target.position, transform.position);
            if(distance < 2f)
            {
                // tấn công
                ChangeState(CharacterState.Attack);
            }
        }
        
        if (distance > radius || distanceToOriginal > maxDistance)
        {
            // quay về vị trí ban đầu
            navMeshAgent.SetDestination(originalePosition);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
            
            // chuyển sang trạng thái đứng yên
            distance = Vector3.Distance(originalePosition, transform.position);
            if(distance < 1f)
            {
                animator.SetFloat("Speed", 0);
            }
            
            // bình thường
            ChangeState(CharacterState.Normal);
        }
    }
    
    // chuyển đổi trạng thái
    private void ChangeState(CharacterState newState)
    {
        // exit current state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
        }
        
        // enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
        }
        
        // update current state
        currentState = newState;
    }
}