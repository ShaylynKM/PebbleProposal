using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private List<string> inventoryItems = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemId)
    {
        inventoryItems.Add(itemId);
    }

    public List<string> GetInventory()
    {
        return inventoryItems;
    }
}