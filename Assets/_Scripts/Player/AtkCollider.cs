using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollider : MonoBehaviour
{
    public GameObject boxAttack;

    private void Awake()
    {
        boxAttack.SetActive(false);
    }
    public void OnAtkCollider()
    {
        boxAttack.SetActive(true);
    }

    public void OffAtkCollider()
    {
        boxAttack.SetActive(false);
    }
}
