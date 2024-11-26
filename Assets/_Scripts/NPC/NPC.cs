using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject npcChatPanel;
    public TextMeshProUGUI chatText;
    public GameObject fKey;
    [HideInInspector] public bool isChating;
    Coroutine coroutine;
    
    // đoạn chat
    public string[] chat;
    // nhiệm vụ
    public QuestItem questItem;

    private void Awake()
    {
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
            coroutine = StartCoroutine(ReadChat());
        }
    }


    IEnumerator ReadChatta()
    {
        foreach (var line in chat)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fKey.SetActive(false);

            if (isChating)
            {
                StopCoroutine(coroutine); // Dừng đoạn chat 
                coroutine = null;
                isChating = false;
            }

            npcChatPanel.SetActive(false);
        }
    }
    //IEnumerator NhiemVu()
    //{
    //    foreach (var line in chat)
    //    {
    //        chatText.text = "";
    //        for (int i = 0; i < line.Length; i++)
    //        {
    //            chatText.text += line[i];
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }

    //    // Giao vật phẩm nhiệm vụ sau khi hội thoại kết thúc
    //    if (questItem != null)
    //    {
    //        GiveQuestItem();
    //    }

    //    // Kết thúc hội thoại
    //    isChating = false;
    //    npcChatPanel.SetActive(false);
    //}

    //void GiveQuestItem()
    //{
    //    // Logic để giao questItem
    //    Debug.Log($"Quest item {questItem.name} được giao cho người chơi!");
    //    // Thêm vào hệ thống nhiệm vụ hoặc vật phẩm người chơi
    //}


    IEnumerator ReadChat()
    {
        foreach (var line in chat)
        {
            chatText.text = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    // Kết thúc hội thoại
                    npcChatPanel.SetActive(false);
                    isChating = false;
                    yield break;
                }

                chatText.text += line[i];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }

        isChating = false;
        npcChatPanel.SetActive(false);
    }
}
