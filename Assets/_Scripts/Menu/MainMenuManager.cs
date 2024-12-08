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

    private void Awake()
    {
        Time.timeScale = 1f;

        nameText.SetActive(true);
        bgrNameText.SetActive(true);
        textClick.SetActive(true);
        buttonPanel.SetActive(false);
    }

    private void Update()
    {
        ShowButton();
    }

    private void ShowButton()
    {
        if(Input.GetMouseButton(0))
        {
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
    #endregion
}
