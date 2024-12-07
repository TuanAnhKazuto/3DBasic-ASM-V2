using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : Health
{
    public PlayerQuest playerQuest;
    public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform target;
    public Slider hpSlider;

    public float radius = 10f;  // Phạm vi phát hiện
    public Vector3 originalPos; // Vị trí ban đầu
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
        GameObject player = GameObject.FindWithTag("Player");

        target = player.GetComponent<Transform>();  

        originalPos = transform.position;

        // Thiết lập vùng di chuyển dựa trên vị trí ban đầu
        moveAreaMin = originalPos - new Vector3(maxDistace, 0, maxDistace);
        moveAreaMax = originalPos + new Vector3(maxDistace, 0, maxDistace);

        hpSlider.value = currentHealth;
        atkCollider.SetActive(false);

        // Quái sẽ không di chuyển cho đến khi phát hiện người chơi
        navMeshAgent.isStopped = false;
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

        var distance = Vector3.Distance(target.position, transform.position);

        // Nếu Player ở trong phạm vi phát hiện, lao đến tấn công
        if (distance <= radius)
        {
            navMeshAgent.SetDestination(target.position); // Di chuyển về phía người chơi
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude); // Điều chỉnh tốc độ animation

            if (distance <= 2f) // Khi gần đến quá trình tấn công
            {
                ChangeState(EnemyState.Attack);
            }
            else
            {
                ChangeState(EnemyState.Normal); // Quái tiếp tục đi tới
            }
        }
        else
        {
            // Nếu không phát hiện Player, quay lại vị trí ban đầu
            animator.SetFloat("Speed", 0); // Dừng animation di chuyển
            ChangeState(EnemyState.Normal);
            navMeshAgent.SetDestination(originalPos);  // Quay lại vị trí ban đầu
            navMeshAgent.isStopped = false;  // Tiếp tục di chuyển
        }

        // Handle Death
        Death();
    }
    // cập nhật nhiệm vụ (Cường)
    void OnDestroy()
    {
        // Cập nhật nhiệm vụ khi zombie bị tiêu diệt
        if (playerQuest != null)
        {
            playerQuest.UpdateQuest("Zombie");
        }
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
            Debug.Log("Enemy died.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            TakeDamage(40);
            hpSlider.value = currentHealth;
        }
    }
}
