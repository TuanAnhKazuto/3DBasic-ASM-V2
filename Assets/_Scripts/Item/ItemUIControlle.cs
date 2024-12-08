using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemUIControlle : MonoBehaviour
{
    public Item item;


    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void Remove()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(this.gameObject);
    }


    public void UseItem()
    {
        Remove();
        

        switch(item.itemType)
        {
            case ItemType.Hp:
                FindObjectOfType<EXP>().IncreaseExp(item.value); 
                break;

            case ItemType.Mp:
                FindObjectOfType<EXP>().IncreaseExp(item.value);
                break;

            case ItemType.Xp:
                FindObjectOfType<EXP>().IncreaseExp(item.value);
                break;

        }

    }


}
