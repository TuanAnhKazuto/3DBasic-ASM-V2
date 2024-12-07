using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject playerCanvas;
    public GameObject statusPanel;
    public GameObject quetPanel;
    public GameObject inventoryPanel;
    public GameObject npcCanvas;
    public GameObject miniMap;
    public GameObject fullMap;

    public bool isViewMap = false;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetActiveTrue();
        SetActiveFalse();
    }

    private void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

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
        fullMap.SetActive(b);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || isViewMap)
        {
            ShowMouse();
        }
        else
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
            fullMap.SetActive(true);
            isViewMap = true;
            Debug.Log("FullMap");
        }
        else if (isViewMap && Input.GetKeyDown(KeyCode.M))
        {
            statusPanel.SetActive(true);
            miniMap.SetActive(true);
            fullMap.SetActive(false);
            isViewMap = false;
        }
    }
}