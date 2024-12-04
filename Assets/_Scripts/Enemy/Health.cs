using UnityEngine;

public class Health : MonoBehaviour
{
    private float maxHealth = 100f;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    //public float CurrentHealth => currentHealth;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
    }
}
