using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollider : MonoBehaviour
{
    public GameObject boxAttack;
    [HideInInspector] public bool isAttacking;

    private void Awake()
    {
        boxAttack.SetActive(false);
        isAttacking = false;
    }
    public void OnAtkCollider()
    {
        boxAttack.SetActive(true);
        isAttacking = true;
    }

    public void OffAtkCollider()
    {
        boxAttack.SetActive(false);
        isAttacking = false;
    }
}
