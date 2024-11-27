using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class NPC1 : MonoBehaviour
{

    public GameObject NPCPanel;
    public TextMeshProUGUI NPCTextContent;
    public string[] content;



    private void Start()
    {
        NPCPanel.SetActive(false);
        NPCTextContent.text = "";
    }

    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.CompareTag("Player"))

        {
            NPCPanel.SetActive(true);

        }
    }
}
