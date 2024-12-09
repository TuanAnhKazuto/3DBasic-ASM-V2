using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject buttonPanel;
    public GameObject nameText;
    public GameObject bgrNameText;
    public GameObject textClick;
    public GameObject controllPanel;
    UIManager uiManager;

    private void Awake()
    {
        GameObject ui = GameObject.FindWithTag("UIManager");
        uiManager = ui.GetComponent<UIManager>();

        Time.timeScale = 1f;

        nameText.SetActive(true);
        bgrNameText.SetActive(true);
        textClick.SetActive(true);
        buttonPanel.SetActive(false);
        controllPanel.SetActive(false);
    }

    private void Update()
    {
        ShowButton();
    }

    private void ShowButton()
    {
        if(Input.GetMouseButton(0))
        {
            uiManager.ShowMouse();
            textClick.SetActive(false);
            buttonPanel.SetActive(true);
            return;
        }
    }

    #region Button Manager
    public void ButtonPlay()
    {
        HideUI();
    }

    void HideUI()
    {
        nameText.SetActive(false);
        bgrNameText.SetActive(false);
        buttonPanel.SetActive(false);
        textClick.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ControllButton()
    {
        controllPanel.SetActive(true);
        buttonPanel.SetActive(false);
    }

    public void ExitControllPanel()
    {
        controllPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }
    #endregion
}
