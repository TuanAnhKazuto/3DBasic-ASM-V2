using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingSlider; // Slider hiển thị tiến trình tải
    public TextMeshProUGUI loadingText; // (Tùy chọn) Hiển thị phần trăm tải dưới dạng text

    private void Start()
    {
        loadingPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadSceneWithProgress(2);
        }
    }

    public void LoadSceneWithProgress(int sceneIndex)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }


    IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        // Bắt đầu tải Scene bất đồng bộ
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // Đảm bảo Scene chỉ được kích hoạt sau khi tải xong
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Giá trị progress nằm trong khoảng [0, 0.9] (0.9 = 90% hoàn tất)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Gán giá trị cho Slider
            loadingSlider.value = progress;

            // (Tùy chọn) Hiển thị phần trăm tải
            if (loadingText != null)
            {
                loadingText.text = $"{(progress * 100):0}%";
            }

            // Kiểm tra nếu tiến trình đã hoàn tất (progress đạt 1)
            if (asyncLoad.progress >= 0.9f)
            {
                // Kích hoạt Scene (chỉ khi bạn muốn)
                asyncLoad.allowSceneActivation = true;
            }

            yield return null; // Đợi đến frame tiếp theo
        }
    }
}