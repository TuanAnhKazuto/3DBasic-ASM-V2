using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour
{
    void Start()
    {
        // Tìm nút EXIT bằng tag 'exit' và gán sự kiện
        GameObject exitButton = GameObject.FindGameObjectWithTag("exit");
        if (exitButton != null)
        {
            Button button = exitButton.GetComponent<Button>();
            button.onClick.AddListener(LoadMainMenu);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy nút với tag 'exit'.");
        }
    }

    public void LoadMainMenu()
    {
        // Tải Scene MainMenu
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading MainMenu...");
    }
}
