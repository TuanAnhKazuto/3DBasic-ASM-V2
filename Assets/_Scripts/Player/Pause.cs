using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    // Reference to the PauseMenu panel
    public GameObject pauseMenu;

    // Flag to check if the game is paused
    private bool isPaused = false;

    void Update()
    {
        // Check if the player presses the "P" key
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                // Unpause the game
                ResumeGame();
            }
            else
            {
                // Pause the game
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        // Show the pause menu and stop time
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;  // Freeze time
        isPaused = true;
    }

    void ResumeGame()
    {
        // Hide the pause menu and resume time
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;  // Resume normal time
        isPaused = false;
    }
}
