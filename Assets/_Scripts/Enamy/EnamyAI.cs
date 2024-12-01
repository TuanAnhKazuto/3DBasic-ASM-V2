using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public Animator animator;
    public DamageZone damageZone;
    public Health health;

    [Header("Settings")]
    [SerializeField] private float radius = 10f; // Bán kính phát hiện mục tiêu
    [SerializeField] private float maxDistance = 50f; // Khoảng cách tối đa từ vị trí ban đầu
    [SerializeField] private float attackDistance = 2f; // Khoảng cách để tấn công

    private Vector3 originalPos;
    private CharacterState curState;

    // State Machine
    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }

    private void Start()
    {
        originalPos = transform.position;
        curState = CharacterState.Normal; // Khởi tạo trạng thái ban đầu

        // Bật isKinematic để quái không bị đẩy
        if (navMeshAgent != null && navMeshAgent.GetComponent<Rigidbody>())
        {
            navMeshAgent.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void Update()
    {
        if (curState == CharacterState.Die)
            return; // Không làm gì nếu đang chết

        float sqrDistanceToOriginal = (originalPos - transform.position).sqrMagnitude;
        float sqrRadius = radius * radius;
        float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;

        if (sqrDistanceToTarget <= sqrRadius)
        {
            // Trong phạm vi phát hiện
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (sqrDistanceToTarget <= attackDistance * attackDistance)
            {
                ChangeState(CharacterState.Attack);
            }
        }
        else if (sqrDistanceToOriginal > maxDistance * maxDistance || sqrDistanceToTarget > sqrRadius)
        {
            // Quay về vị trí ban đầu nếu quá xa
            navMeshAgent.SetDestination(originalPos);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (Vector3.Distance(transform.position, originalPos) < 1f)
            {
                navMeshAgent.ResetPath(); // Dừng hoàn toàn
                animator.SetFloat("Speed", 0);
                ChangeState(CharacterState.Normal);
            }
        }
    }

    private void ChangeState(CharacterState newState)
    {
        if (curState == newState)
            return;

        // Exit current state
        switch (curState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                break;
            case CharacterState.Attack:
                damageZone.EndAttack();
                break;
        }

        // Enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                Debug.Log("Enemy ở trạng thái bình thường");
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                damageZone.BeginAttack();
                Debug.Log("Enemy đang tấn công!");
                break;
            case CharacterState.Die:
                animator.SetTrigger("Die");
                navMeshAgent.enabled = false;
                Destroy(gameObject, 5f); // Xóa đối tượng sau 5 giây
                Debug.Log("Enemy đã chết!");
                break;
        }

        curState = newState; // Cập nhật trạng thái
    }

    public void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += originalPos;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
            Debug.Log("Enemy đang đi lang thang!");
        }
    }

    public void TakeDamage(float amount)
    {
        if (curState == CharacterState.Die)
            return;

        health.TakeDamage(amount);
        if (health.CurrentHealth <= 0)
        {
            ChangeState(CharacterState.Die);
        }
    }

    // Xử lý va chạm mà không đẩy player
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem quái vật có va vào người chơi không
        if (other.CompareTag("Player"))
        {
            // Xử lý va chạm mà không làm thay đổi vị trí của player
            // Ví dụ: kích hoạt một hành động tấn công hoặc hư hại cho player
            Debug.Log("Quái vật va vào người chơi nhưng không đẩy!");
        }
    }
}
