using UnityEditor.Timeline.Actions;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject playerCanvas;
    public GameObject npcCanvas;

    public GameObject statusPanel;
    public GameObject quetPanel;
    public GameObject inventoryPanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    public GameObject miniMap;
    public GameObject fullMap;

    [HideInInspector]
    public bool isViewMap = false;
    public bool isGameOverPanelOn = false;



    private void Start()
    {
        isGameOverPanelOn = false;
        Time.timeScale = 1f;
        
        HideMouse();

        SetActiveTrue();
        SetActiveFalse();
    }

    #region Mouse Setting
    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    #region Set Active When Start
    private void SetActiveTrue(bool b = true)
    {
        playerCanvas.SetActive(b);
        npcCanvas.SetActive(b);
        quetPanel.SetActive(b);
        statusPanel.SetActive(b);

        miniMap.SetActive(b);
    }

    private void SetActiveFalse(bool b = false)
    {
        inventoryPanel.SetActive(b);
        gameOverPanel.SetActive(b);
        victoryPanel.SetActive(b);
        fullMap.SetActive(b);
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ShowMouse();
        }
        
        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            HideMouse();
        }

        ViewFullMap();
    }

    public void ViewFullMap()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isViewMap)
        {
            statusPanel.SetActive(false);
            miniMap.SetActive(false);
            quetPanel.SetActive(false);
            fullMap.SetActive(true);
            isViewMap = true;
        }
        else if (isViewMap && Input.GetKeyDown(KeyCode.M))
        {
            statusPanel.SetActive(true);
            miniMap.SetActive(true);
            quetPanel.SetActive(true);
            fullMap.SetActive(false);
            isViewMap = false;
        }
    }

    public void OnGameOverPanel()
    {
        statusPanel.SetActive(false);
        miniMap.SetActive(false);
        quetPanel.SetActive(false);
        fullMap.SetActive(false);
        gameOverPanel.SetActive(true);
        ShowMouse();
        Invoke(nameof(StopGame), 0.2f);
    }

    public void OnVictoryPanel()
    {
        statusPanel.SetActive(false);
        miniMap.SetActive(false);
        quetPanel.SetActive(false);
        fullMap.SetActive(false);
        victoryPanel.SetActive(true);
        ShowMouse();
        Invoke(nameof(StopGame), 0.2f);
    }

    public void OnLoadingScene()
    {
        playerCanvas.SetActive(false);
        npcCanvas.SetActive(false);
    }

    void StopGame()
    {
        Time.timeScale = 0;
    }
}