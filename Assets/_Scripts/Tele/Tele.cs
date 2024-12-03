using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public UIManager ui;
    public bool isLoadScene;
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng chạm vào có tag "Player"
        if (other.CompareTag("Player"))
        {
            isLoadScene = true;
            ui.NextLevel();
        }
    }
}