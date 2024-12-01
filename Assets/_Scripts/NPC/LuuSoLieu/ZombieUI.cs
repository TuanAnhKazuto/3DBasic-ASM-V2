using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieUI : MonoBehaviour
{
    public ZombieManager zombieManager;
    public TextMeshProUGUI zombiesKilledText;

    void Update()
    {
        // Cập nhật số lượng zombie đã giết lên giao diện người dùng
        zombiesKilledText.text = "Zombies Killed: " + zombieManager.GetZombiesKilled();
    }
}
