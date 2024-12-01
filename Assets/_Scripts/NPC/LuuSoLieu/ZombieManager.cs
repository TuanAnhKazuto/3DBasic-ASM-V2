using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{

    
    private int zombiesKilled;

    // Dữ liệu sẽ được lưu vào PlayerPrefs (lưu trữ tạm thời)
    private const string ZombiesKilledKey = "ZombiesKilled";

    // Hàm khởi tạo, đọc dữ liệu từ PlayerPrefs khi game bắt đầu
    void Start()
    {
        zombiesKilled = PlayerPrefs.GetInt(ZombiesKilledKey, 0); // 0 là giá trị mặc định
        Debug.Log("Zombies killed so far: " + zombiesKilled);
    }

    // Hàm gọi khi zombie bị giết
    public void OnZombieKilled()
    {
        zombiesKilled++;
        // Lưu số lượng zombie đã giết vào PlayerPrefs
        PlayerPrefs.SetInt(ZombiesKilledKey, zombiesKilled);
        PlayerPrefs.Save();

        Debug.Log("Zombie killed! Total: " + zombiesKilled);
    }

    // Hàm để lấy số lượng zombie đã giết
    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

    // Hàm reset số lượng zombie
    public void ResetZombiesKilled()
    {
        zombiesKilled = 0;
        PlayerPrefs.SetInt(ZombiesKilledKey, zombiesKilled);
        PlayerPrefs.Save();
    }
}

