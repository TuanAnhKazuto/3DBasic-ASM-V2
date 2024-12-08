using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIControlle : MonoBehaviour
{
    public Item item;



    public void Remove()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(this.gameObject);
    }

    public void UseItem()
    {
        //Optional
        Remove();

        switch(item.itemType)
        {
            case ItemType.Glass:
                FindObjectOfType<EXP>().IncreaseExp(item.value); 
                break;
            case ItemType.Bottle:
                FindObjectOfType<EXP>().IncreaseExp(item.value);
                break;

        }

    }


}
