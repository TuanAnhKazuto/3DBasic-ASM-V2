using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollider : MonoBehaviour
{
    public GameObject boxAttack;
    public PlayerSound sound;

    private void Awake()
    {
        boxAttack.SetActive(false);
    }
    public void OnAtkCollider()
    {
        boxAttack.SetActive(true);
        sound.soundAttack01.Play();

        
    }

    public void OffAtkCollider()
    {
        boxAttack.SetActive(false);
        sound.soundAttack01.Stop();
    }
}
