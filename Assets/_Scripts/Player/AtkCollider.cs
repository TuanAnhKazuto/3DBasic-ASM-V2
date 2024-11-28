using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollider : MonoBehaviour
{
    public GameObject boxAttack;
    //[HideInInspector] public bool isAttacking;
    //[HideInInspector] public bool whenAttack;

    private void Awake()
    {
        boxAttack.SetActive(false);
        //isAttacking = false;
        //whenAttack = false;
    }
    public void OnAtkCollider()
    {
        boxAttack.SetActive(true);
        //isAttacking = true;
        //whenAttack = true;
        //Invoke(nameof(StopBool), 0.1f);
    }

    //public void StopBool ()
    //{
    //    whenAttack = false;
    //}

    public void OffAtkCollider()
    {
        boxAttack.SetActive(false);
        //isAttacking = false;
    }
}
