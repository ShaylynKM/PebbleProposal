using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the panel that represents the inventory

    public GameObject inventorySlotPrefab; // Prefab for the inventory slot

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

        // If the inventory is open, update the slots with the collected rock sprites
        if (isInventoryOpen)
        {
            UpdateInventorySlots();
        }
    }

    private void UpdateInventorySlots()
    {
        // Get the player's inventory
        PlayerInventory playerInventory = PlayerInventory.instance;

        // Get the reference to the InventorySlots transform
        Transform inventorySlotsTransform = inventoryPanel.transform.Find("InventorySlots");

        // Clear existing inventory slots
        foreach (Transform child in inventorySlotsTransform)
        {
            Destroy(child.gameObject);
        }

        // Create new inventory slots for each collected rock
        foreach (string itemId in playerInventory.GetInventory())
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventorySlotsTransform);
            Image slotImage = slotObject.GetComponent<Image>();

            // Find the rock object based on the item ID and set its sprite to the inventory slot image
            Rocks rock = FindRockById(itemId);
            if (rock != null)
            {
                slotImage.sprite = rock.itemSprite;
                Debug.Log("Assigned sprite to inventory slot: " + rock.itemSprite.name);
            }
            else
            {
                Debug.LogWarning("Rock not found for item ID: " + itemId);
            }
        }
    }

    private Rocks FindRockById(string itemId)
    {
        Rocks[] rocks = FindObjectsOfType<Rocks>();

        foreach (Rocks rock in rocks)
        {
            if (rock.itemId == itemId)
            {
                Debug.Log("Found rock with matching item ID: " + itemId); // Add this line to print the item ID
                return rock;
            }
        }

        Debug.LogWarning("Rock not found for item ID: " + itemId);
        return null; // Return null if the rock is not found
    }

    public void CloseInventory()
    {
        ToggleInventory();
    }
}
