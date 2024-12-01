using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng chạm vào có tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã chạm vào tele! Chuyển sang Scene1...");
            // Chuyển đến Scene có tên "Scene1"
            SceneManager.LoadScene("hehe");
        }
    }
}