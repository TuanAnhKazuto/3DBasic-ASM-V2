using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    public Animator animator;
    public float cooldownTime = 2f;
    private float nextFireTime = 0;
    public static float noOfClick = 0f;
    float lastClickedTime = 0f;
    float maxComboDelay = 1f;

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA1"))
        {
            animator.SetBool("Attack01", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA2"))
        {
            animator.SetBool("Attack02", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA3"))
        {
            animator.SetBool("Attack03", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA4"))
        {
            animator.SetBool("Attack04", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA5"))
        {
            animator.SetBool("Attack05", false);
            noOfClick = 0;
        }

        if(Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClick = 0;
        }
        if(Time.time > nextFireTime)
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;

        noOfClick++;
        if(noOfClick == 1)
        {
            animator.SetBool("Attack01", true);
        }
        noOfClick = Mathf.Clamp(noOfClick, 0, 3);

        if(noOfClick >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA1"))
        {
            animator.SetBool("Attack01", false);
            animator.SetBool("Attack02", true);
        }
        if(noOfClick >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA2"))
        {
            animator.SetBool("Attack02", false);
            animator.SetBool("Attack03", true);
        }
        if (noOfClick >= 4 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA3"))
        {
            animator.SetBool("Attack03", false);
            animator.SetBool("Attack04", true);
        }
        if (noOfClick >= 5 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA4"))
        {
            animator.SetBool("Attack04", false);
            animator.SetBool("Attack05", true);
        }
        if (noOfClick >= 6 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA5"))
        {
            animator.SetBool("Attack04", false);
            animator.SetBool("Attack05", true);
        }

    }
}
