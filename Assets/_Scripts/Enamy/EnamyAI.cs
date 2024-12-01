using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;
    public Animator animator;
    public DamegeZone damageZone;
    public Health health;
   


    // state machine
    public enum CharacterState
    { Normal, Attack, Die }

    public CharacterState curState; // trạng thái hiện tại
    private Vector3 originalePosition;

    private void Start()
    {
        originalPos = transform.position;

    }

    private void Update()
    {
        // Khoảng cách từ vị trí hiện tại đến vị trí ban đầu
        var distanceToOriginal = Vector3.Distance(originalPos, transform.position);
        // Khoảng cách từ vị trí hiện tại đến vị trí mục tiêu

        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius)
        {
            // di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distance < 2f)
            {
                // tan cong
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

            // binh thuong
            ChangeState(CharacterState.Normal);
        }
    }
    // chuyển đổi trạng thái
    private void ChangeState(CharacterState newState)
    {


        // B1: exit curState
        switch (curState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
        }

        // B2: enter newState
        switch (newState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                break;
            case CharacterState.Attack:
                damageZone.BeginAttack();
                break;
            case CharacterState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 5f);
                break;
        }

        // B3: Update state
        curState = newState;
    }

    public void Wandar()
    {
        var randomDirection = Random.insideUnitSphere * radius;
        randomDirection += originalePosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        var finalPosition = hit.position;
        navMeshAgent.SetDestination(finalPosition);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
}

public class DamageZone
{
    internal void BeginAttack()
    {
        throw new System.NotImplementedException();
    }
    internal void EndAttack()
    {
        throw new System.NotImplementedException();
    }

}
