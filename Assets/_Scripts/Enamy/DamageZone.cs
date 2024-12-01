using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 10; // Sát thương gây ra mỗi đòn đánh
    public Collider attackCollider; // Collider cho vùng tấn công

    private void Awake()
    {
        // Tự động tìm Collider nếu chưa gán
        if (attackCollider == null)
        {
            attackCollider = GetComponent<Collider>();
            if (attackCollider != null)
            {
                Debug.Log("Tự động gán Attack Collider.");
            }
            else
            {
                Debug.LogError("Không tìm thấy Collider! Vui lòng thêm Collider và bật Is Trigger.");
            }
        }
    }

    /// <summary>
    /// Bật vùng sát thương.
    /// </summary>
    public void BeginAttack()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true; // Bật vùng sát thương
            Debug.Log("Bắt đầu tấn công!");
        }
        else
        {
            Debug.LogWarning("Attack Collider chưa được gán! Vui lòng gán trong Inspector.");
        }
    }

    /// <summary>
    /// Tắt vùng sát thương.
    /// </summary>
    public void EndAttack()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Tắt vùng sát thương
            Debug.Log("Kết thúc tấn công!");
        }
        else
        {
            Debug.LogWarning("Attack Collider chưa được gán! Vui lòng gán trong Inspector.");
        }
    }

    /// <summary>
    /// Xử lý va chạm với đối tượng trong vùng sát thương.
    /// </summary>
    /// <param name="other">Đối tượng va chạm.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Kiểm tra Tag của đối tượng
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Gây sát thương
                Debug.Log($"Gây sát thương {damage} cho {other.name}.");
            }
        }
    }
}
