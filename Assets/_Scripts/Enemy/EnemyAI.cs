using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Health
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;

    public Animator animator;

    public GameObject atkCollider;

    // state machine
    public enum EnemyState
    { Normal, Attack, Die }

    public EnemyState curState; // trạng thái hiện tại

    private void Start()
    {
        originalPos = transform.position;
        atkCollider.SetActive(false);
    }

    private void Update()
    {
        if (target != null) // Kiểm tra nếu có mục tiêu
        {
            // Xoay mặt đối tượng về phía mục tiêu
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        }
        else
        {
            return;
        }

        if (curState == EnemyState.Die) // Nếu đang ở trạng thái chết, không thực hiện gì thêm
        {
            return;
        }

        // Khoảng cách từ vị trí hiện tại đến vị trí ban đầu
        var distanceToOriginal = Vector3.Distance(originalPos, transform.position);

        // Khoảng cách từ vị trí hiện tại đến vị trí mục tiêu
        var distance = Vector3.Distance(target.position, transform.position);

        if (distance <= radius && distanceToOriginal <= maxDistace) // Nếu mục tiêu trong phạm vi
        {
            // Di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distance <= 2f) // Nếu đến gần mục tiêu, tấn công
            {
                ChangeState(EnemyState.Attack);
            }
        }

        if (distance > radius || distanceToOriginal > maxDistace) // Nếu mục tiêu ra ngoài phạm vi
        {
            // Quay về vị trí ban đầu
            navMeshAgent.SetDestination(originalPos);

            // Chuyển sang trạng thái đứng yên khi về đến vị trí ban đầu
            if (Vector3.Distance(originalPos, transform.position) < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            ChangeState(EnemyState.Normal);
        }

        Death();
    }

    public void StartAttack()
    {
        atkCollider.SetActive(true);
    }

    public void EndAttack()
    {
        atkCollider.SetActive(false);
    }

    private void ChangeState(EnemyState newState)
    {
        switch (curState)
        {
            case EnemyState.Normal:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Die:
                break;
        }

        switch (newState)
        {
            case EnemyState.Normal:
                break;
            case EnemyState.Attack:
                animator.SetTrigger("Attack");
                break;
            case EnemyState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 1.5f);
                break;
        }

        curState = newState;
    }

    private void Death()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            ChangeState(EnemyState.Die);
            Debug.Log("Is die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            TakeDamage(20);
            Debug.Log(currentHealth);
        }
    }
}
