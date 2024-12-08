using UnityEngine;
using UnityEngine.UI;

public class ResumeGame : MonoBehaviour
{
    void Start()
    {
        // Tìm Button bằng tag 'resume' và gán sự kiện
        GameObject resumeButton = GameObject.FindGameObjectWithTag("resume");
        if (resumeButton != null)
        {
            Button button = resumeButton.GetComponent<Button>();
            button.onClick.AddListener(Resume);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy nút với tag 'resume'.");
        }
    }

    public void Resume()
    {
        // Tắt Panel PauseMenu
        GameObject pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy PauseMenu với tag 'Pause'.");
        }

        // Tiếp tục game
        Time.timeScale = 1f;
        Debug.Log("Game resumed!");
    }
}
