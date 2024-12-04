using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollider : MonoBehaviour
{
    public BoxCollider boxAttack;
    public PlayerSound sound;
    public CharacterController player;
    private void Awake()
    {
        boxAttack.enabled = false;
    }
    public void OnAtkCollider()
    {
        boxAttack.enabled = true;
        sound.soundAttack01.Play();
    }

    public void OffAtkCollider()
    {
        boxAttack.enabled = false;
        sound.soundAttack01.Stop();
    }
}
