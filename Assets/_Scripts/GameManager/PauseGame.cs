using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    UIManager uiManager;

    private void Start()
    {
        GameObject ui = GameObject.FindWithTag("UIManager");
        uiManager = ui.GetComponent<UIManager>();
    }

    public void Replay()
    {
        uiManager.HidePauseGamePanle();
    }

    
}
