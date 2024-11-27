using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public CharacterMovement player;
    public AtkCollider atkCollider;
    public Slider staminaBar;

    float maxStamina = 100;
    public float curStamina;

    private void Awake()
    {
        curStamina = maxStamina;
        staminaBar.value = curStamina;
    }

    private void Update()
    {
        RecoverStamina();
        SubStaminaWhenRun();
        SubStaminaWhenAttack();
    }

    void RecoverStamina()
    {
        if(!player.isRunning)
        {
            UpdateStamina(3);
        }
    }

    void SubStaminaWhenRun()
    {
        if (player.isRunning)
        {
            UpdateStamina(-3);
        }
    }

    void SubStaminaWhenAttack()
    {
        if (atkCollider.isAttacking)
        {
            UpdateStamina(15f);
        }
        else
        {
            return;
        }
        
    }

    public void UpdateStamina(float count)
    {
        curStamina += count * Time.deltaTime;
        staminaBar.value = curStamina;
    }
}
