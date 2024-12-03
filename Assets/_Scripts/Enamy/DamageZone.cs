using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public Collider damageCollider; // Collider cho vùng sát thương
    public int damageAmount = 20; // Số sát thương gây ra

    public string targetTag; // Tag của mục tiêu (ví dụ: "Enemy")
    public List<Collider> collidersTargets = new List<Collider>(); // Danh sách các collider của mục tiêu

    void Start()
    {
        damageCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag) && !collidersTargets.Contains(other))
        {
            collidersTargets.Add(other);
            var enemyAI = other.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(damageAmount);
            }
        }
    }

    public void BeginAttack()
    {
        collidersTargets.Clear();
        damageCollider.enabled = true;
        Debug.Log("Bắt đầu tấn công!");
    }

    public void EndAttack()
    {
        collidersTargets.Clear();
        damageCollider.enabled = false;
        Debug.Log("Kết thúc tấn công!");
    }
}
