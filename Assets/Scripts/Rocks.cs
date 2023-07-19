using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public int itemId; // Unique identifier for the collectible item
    public Sprite itemSprite; // Sprite or model representing the collectible item
    public string itemDescription; // Description of the collectible item
    public bool isCollected;

    private void Start()
    {
        isCollected = false;
    }

    // When the player interacts with the collectible item
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Item ID: " + itemId); // Add this line to print the item ID

                isCollected = true;
                // Add picking rock up animation and sound effect

                // Add the item to the player's inventory
                //PlayerInventory.instance.AddItem(itemId);

                // Destroy the collectible item
                Destroy(gameObject);

                Debug.Log(isCollected);
            }
        }
    }
}

