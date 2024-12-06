using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<Item> items = new List<Item>();

    public Transform itemContentPane;
    public GameObject itemPrefab;

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
            //return;
        }    

        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
        DisplayInventory();
    }    

    public void DisplayInventory()
    {
        foreach (Transform item in itemContentPane)
        {
            Destroy(item.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemContentPane);
            var itemImager = obj.transform.Find("Image").GetComponent<Image>();
            itemImager.sprite = item.inmage;

            Debug.Log("add item done");
        }
    }

}
