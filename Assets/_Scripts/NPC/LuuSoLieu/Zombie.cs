using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieManager zombieManager;

    // Hàm để chết khi bị tấn công hoặc bị tiêu diệt
    void Die()
    {
        // Gọi hàm để cập nhật số lượng zombie bị giết
        zombieManager.OnZombieKilled();

        // Sau đó có thể thêm logic chết của zombie như hiệu ứng, âm thanh...
        Destroy(gameObject);
    }
}
