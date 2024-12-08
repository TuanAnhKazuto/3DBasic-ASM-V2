using UnityEngine;
using UnityEngine.UI;

public class SettingPanelController : MonoBehaviour
{
    //private GameObject settingPanel;

    //void Start()
    //{
    //    // Tìm Panel Setting bằng tag 'Setting2'
    //    settingPanel = GameObject.FindGameObjectWithTag("Setting2");
    //    if (settingPanel != null)
    //    {
    //        settingPanel.SetActive(false); // Ban đầu ẩn Panel Setting
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Không tìm thấy Panel với tag 'Setting2'.");
    //    }

    //    // Tìm nút Setting và gán sự kiện
    //    GameObject settingButton = GameObject.FindGameObjectWithTag("Setting");
    //    if (settingButton != null)
    //    {
    //        Button button = settingButton.GetComponent<Button>();
    //        button.onClick.AddListener(ShowSettingPanel);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Không tìm thấy nút với tag 'Setting'.");
    //    }
    //}

    //void Update()
    //{
    //    // Tắt Panel khi nhấn ESC
    //    if (Input.GetKeyDown(KeyCode.Escape) && settingPanel.activeSelf)
    //    {
    //        CloseSettingPanel();
    //    }
    //}



    //public void ShowSettingPanel()
    //{
    //    if (settingPanel != null)
    //    {
    //        settingPanel.SetActive(true); // Hiển thị Panel Setting
    //    }
    //}

    //public void CloseSettingPanel()
    //{
    //    if (settingPanel != null)
    //    {
    //        settingPanel.SetActive(false); // Ẩn Panel Setting
    //    }
    //}
    public GameObject setting2;

    private void Start()
    {
        setting2.SetActive(false);
    }

    public void ShowSetting2()
    {
        setting2.SetActive(true);
    }
}
