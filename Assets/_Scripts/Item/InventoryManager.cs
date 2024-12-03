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
        }    

        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }    

    public void DisplayInventory()
    {
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemContentPane);
            var itemImager = obj.transform.Find("Image").GetComponent<Image>();
            itemImager.sprite = item.inmage;
        }
    }

}
