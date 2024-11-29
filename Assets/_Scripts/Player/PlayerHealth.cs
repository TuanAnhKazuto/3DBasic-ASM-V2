using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public int curHp;
    public int maxHp;

    private void Awake()
    {
        maxHp = 100;
        curHp = maxHp;
        healthBar.UpdateBar(maxHp, curHp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        healthBar.UpdateBar(maxHp, curHp);
    }
}
