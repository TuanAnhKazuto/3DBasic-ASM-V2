using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    // luu so luong zombie tieu diet
    private int zombiesKilled;
    private const string ZombiesKilledKey = "ZombiesKilled";


    void Start()
    {
        zombiesKilled = PlayerPrefs.GetInt(ZombiesKilledKey, 0); 
        Debug.Log("Zombies killed so far: " + zombiesKilled);
    }

    // Hàm gọi khi zombie bị giết
    public void OnZombieKilled()
    {
        zombiesKilled++;
       
        PlayerPrefs.SetInt(ZombiesKilledKey, zombiesKilled);
        PlayerPrefs.Save();

        Debug.Log("Zombie killed! Total: " + zombiesKilled);
    }

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

    // reset số lượng zombie
    public void ResetZombiesKilled()
    {
        zombiesKilled = 0;
        PlayerPrefs.SetInt(ZombiesKilledKey, zombiesKilled);
        PlayerPrefs.Save();
    }
}

