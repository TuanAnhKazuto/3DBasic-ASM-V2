﻿using System.Collections;
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
            coroutine = StartCoroutine(ReadChat());
        }
    }


    IEnumerator ReadChat()
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
}
