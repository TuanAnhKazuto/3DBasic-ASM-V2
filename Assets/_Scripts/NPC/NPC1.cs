using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    public GameObject npcChatPanel;
    public TextMeshProUGUI chatText;
    public GameObject fKey;
    [HideInInspector] public bool isChating;
    Coroutine coroutine;

    public string[] content;


    private void Awake()
    {
        fKey.SetActive(true);

        fKey.SetActive(false);
        npcChatPanel.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fKey.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F) && !isChating)
        {
            isChating = true; // Đánh dấu đang trong trạng thái hội thoại
            fKey.SetActive(false);
            npcChatPanel.SetActive(true);
            coroutine = StartCoroutine(Readcontent());
        }
    }


    IEnumerator Readcontent()
    {
        foreach (var line in content)
        {
            chatText.text = "";
            for (int i = 0; i < line.Length; i++)
            {
                chatText.text += line[i];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SkipContent()
    {
        StopCoroutine(coroutine);
        // Hiện nút nhiệm vụ
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcChatPanel.SetActive(false);
            StopCoroutine(coroutine);
            // Kiểm tra nếu coroutine không null trước khi dừng nó
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null; // Đặt lại giá trị để tránh lỗi lần sau
            }
        }
    }
}
