using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the panel that represents the inventory
    private bool isInventoryOpen = false; // Flag to track if the inventory is open

    private void Start()
    {
        // Hide the inventory panel initially
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // Check if the "I" key is pressed to toggle the inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        // Show or hide the inventory panel based on the inventory state
        inventoryPanel.SetActive(isInventoryOpen);
    }

    public void CloseInventory()
    {
        ToggleInventory();
    }
}
