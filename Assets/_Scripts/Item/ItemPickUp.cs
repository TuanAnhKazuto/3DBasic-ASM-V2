using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
   public Item item;

    // nhặt Item lên
    void PickUp()
    {
        // Detroy
        Destroy(gameObject);
        //Add Inventory
        InventoryManager.Instance.Add(item);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }    
    }
}
