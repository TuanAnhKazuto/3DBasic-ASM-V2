using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject buttonPanel;
    public GameObject textClick;

    private void Awake()
    {
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
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    #endregion
}
