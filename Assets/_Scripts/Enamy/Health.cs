using UnityEngine;

public class Health : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;

    public float CurrentHealth => currentHealth;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

    }
}
