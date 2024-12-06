using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : Health
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public Slider hpSlider;

    public float radius = 10f;
    public Vector3 originalPos;
    public float maxDistace = 50f;

    public Animator animator;

    public GameObject atkCollider;

    // Vật phẩm rơi ra khi quái chết
    public GameObject itemPrefab;

    // state machine
    public enum EnemyState
    { Normal, Attack, Die }

    public EnemyState curState; // trạng thái hiện tại

    public Vector3 moveAreaMin; // Corner 1 của khu vực di chuyển
    public Vector3 moveAreaMax; // Corner 2 của khu vực di chuyển

    private void Start()
    {
        originalPos = transform.position;
        hpSlider.value = currentHealth;
        atkCollider.SetActive(false);

        // Bắt đầu di chuyển trong khu vực nhỏ
        StartCoroutine(MoveRandomly());
    }

    private void Update()
    {
        if (target != null)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        }
        else
        {
            return;
        }

        if (curState == EnemyState.Die)
        {
            return;
        }

        var distanceToOriginal = Vector3.Distance(originalPos, transform.position);
        var distance = Vector3.Distance(target.position, transform.position);

        // Nếu Player ở trong phạm vi phát hiện, lao đến tấn công
        if (distance <= radius)
        {
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude); // Điều chỉnh tốc độ animation

            if (distance <= 2f)
            {
                ChangeState(EnemyState.Attack);
            }
        }
        else
        {
            // Nếu không phát hiện Player, tiếp tục di chuyển ngẫu nhiên trong khu vực nhỏ
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude); // Điều chỉnh tốc độ animation
            ChangeState(EnemyState.Normal);
        }

        // Handle Death
        Death();
    }

    private IEnumerator MoveRandomly()
    {
        while (curState != EnemyState.Die)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(moveAreaMin.x, moveAreaMax.x),
                transform.position.y, // Giữ chiều cao của zombie
                Random.Range(moveAreaMin.z, moveAreaMax.z)
            );

            navMeshAgent.SetDestination(randomPos);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude); // Điều chỉnh tốc độ animation
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Di chuyển ngẫu nhiên mỗi 2-5 giây
        }
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

                // Rơi vật phẩm tại vị trí quái chết
                if (itemPrefab != null)
                {
                    Instantiate(itemPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject, 1.8f);
                break;
        }

        curState = newState;
    }

    private void Death()
    {
        if (currentHealth <= 0)
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
            TakeDamage(30);
            hpSlider.value = currentHealth;
        }
    }
}
