using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{

    public List<QuestItem> questItems = new List<QuestItem>();

    public PaneQuest PlayerQuestsPanel;

    // Nhận nhiệm vụ 
   
    public void TakeQuest(QuestItem questItem)
    {

        var check = questItems
                    .FirstOrDefault(x => x.QuetsItemName==
                                questItem.QuetsItemName);

        if (check != null) 
        questItems.Add(questItem);

        Debug.Log("Nhận nhiệm vụ: " + questItem.QuetsItemName);
        // hiển thị
        PlayerQuestsPanel.ShowAllQuestItem(questItems);
        
    }    
}
