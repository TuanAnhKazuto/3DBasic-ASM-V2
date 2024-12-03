using UnityEngine;

public class GolemAI : MonoBehaviour
{
    public Transform player; // Gắn Player vào đây
    public float attackRange = 2.0f; // Khoảng cách để bắt đầu tấn công
    public float moveSpeed = 3.0f; // Tốc độ di chuyển

    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Trong tầm tấn công
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("Attack"); // Chuyển sang trạng thái Attack
            }
        }
        else
        {
            // Ngoài tầm tấn công, di chuyển về phía Player
            isAttacking = false;
            animator.SetTrigger("Run"); // Chuyển sang trạng thái Run
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
