using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : Health
{
    [HideInInspector] public PlayerQuest playerQuest;
    public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform target;
    public Slider hpSlider;

    public float radius = 10f;  // Phạm vi phát hiện
    public Vector3 originalPos; // Vị trí ban đầu
    public float maxDistace = 50f;

    public Animator animator;

    public BoxCollider atkCollider;

    // Vật phẩm rơi ra khi quái chết
    //public GameObject itemPrefab;

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

        GameObject plQuest = GameObject.FindWithTag("Player");
        playerQuest = plQuest.GetComponent<PlayerQuest>();

        originalPos = transform.position;

        // Kiểm tra nếu vị trí ban đầu nằm trên NavMesh
        if (NavMesh.SamplePosition(originalPos, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            originalPos = hit.position; // Đặt lại vị trí trên NavMesh
            navMeshAgent.enabled = true; // Bật NavMeshAgent
            navMeshAgent.Warp(originalPos); // Đặt vị trí ban đầu chính xác
        }
        else
        {
            Debug.LogError("Enemy không nằm trên NavMesh, kiểm tra vị trí ban đầu.");
        }

        hpSlider.value = currentHealth;
        atkCollider.enabled = false;

        navMeshAgent.isStopped = false; // Kích hoạt di chuyển chỉ khi NavMeshAgent hợp lệ
    }


    private void Update()
    {
        if (curState == EnemyState.Die || !navMeshAgent.isOnNavMesh)
        {
            return; // Không thực hiện gì nếu Enemy đã chết hoặc không nằm trên NavMesh
        }

        if (target != null)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        }

        var distance = Vector3.Distance(target.position, transform.position);

        if (distance <= radius)
        {
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.SetDestination(target.position);
            }

            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distance <= 2f)
            {
                ChangeState(EnemyState.Attack);
            }
            else
            {
                ChangeState(EnemyState.Normal);
            }
        }
        else
        {
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.SetDestination(originalPos);
            }

            animator.SetFloat("Speed", 0);
            ChangeState(EnemyState.Normal);
        }

        Death();
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
                //if (itemPrefab != null)
                //{
                //    Instantiate(itemPrefab, transform.position, Quaternion.identity);
                //}
                Destroy(gameObject, 1.8f);
                break;
        }

        curState = newState;
    }

    private void Death()
    {
        if (currentHealth <= 0)
        {
            playerQuest.UpdateQuest("Zombie");
            currentHealth = 0;
            EndAttack();
            ChangeState(EnemyState.Die);
            Debug.Log("Enemy die.");
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

    public void OnAttack()
    {
        atkCollider.enabled = true;
    }

    public void EndAttack()
    {
        atkCollider.enabled = false;
    }
}
