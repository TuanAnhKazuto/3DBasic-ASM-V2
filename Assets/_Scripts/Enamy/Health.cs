using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHP;
    public float currentHP;

     public virtual void TakeDamage(float damege)
    {
        currentHP -= damege;
        currentHP = Mathf.Max(0, currentHP);
     
    }
}


