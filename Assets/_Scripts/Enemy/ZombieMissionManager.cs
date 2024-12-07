using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieMissionManager : MonoBehaviour
{
    public int zombieKillGoal = 1; // Số zombie cần tiêu diệt
    private int zombiesKilled = 0; // Số zombie đã tiêu diệt
    public TextMeshProUGUI missionStatusText; // UI hiển thị trạng thái nhiệm vụ

    void Start()
    {
        UpdateMissionStatus();
    }

    public void ZombieKilled()
    {
        zombiesKilled++;
        UpdateMissionStatus();

        if (zombiesKilled >= zombieKillGoal)
        {
            MissionComplete();
        }
    }

    void UpdateMissionStatus()
    {
        if (missionStatusText != null)
        {
            missionStatusText.text = $"Zombies killed: {zombiesKilled}/{zombieKillGoal}";
        }
    }

    void MissionComplete()
    {
        Debug.Log("Mission Complete!");
        if (missionStatusText != null)
        {
            missionStatusText.text = "Mission Complete!";
        }

       
    }
}
