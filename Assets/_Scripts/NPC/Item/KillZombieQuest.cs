using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZombieQuest : MonoBehaviour
{
   // Số Zombie cần giết để hoàn thành nhiệm vụ
    public int zombiesToKill = 5;

    // Biến đếm số Zombie đã giết
    private int zombiesKilled = 0;

    // Trạng thái hoàn thành nhiệm vụ
    private bool isQuestCompleted = false;

    // Gọi khi người chơi giết được một Zombie
    public void OnZombieKilled()
    {
        if (isQuestCompleted)
            return;

        zombiesKilled++;
        Debug.Log("Zombie đã giết: " + zombiesKilled + "/" + zombiesToKill);

        // Kiểm tra nếu đã đủ số lượng Zombie
        if (zombiesKilled >= zombiesToKill)
        {
            CompleteQuest();
        }
    }

    // Hàm hoàn thành nhiệm vụ
    private void CompleteQuest()
    {
        isQuestCompleted = true;
        Debug.Log("Nhiệm vụ hoàn thành! Bạn đã giết đủ " + zombiesToKill + " Zombie.");
    }

    // Getter trạng thái hoàn thành nhiệm vụ
    public bool IsQuestCompleted()
    {
        return isQuestCompleted;
    }
}


