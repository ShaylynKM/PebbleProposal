using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    [SerializeField]
    public int slotId;
    //public Sprite rockSprite;

    public Image imageComponent;
    public Rocks rocks;

    void Start()
    {
        imageComponent = GetComponent<Image>();
    }
    void Update()
    {
        Debug.Log(slotId + rocks.itemId);
        Debug.Log(rocks.isCollected);
        if (slotId == rocks.itemId && rocks.isCollected == true)
        {
            imageComponent.sprite = rocks.itemSprite;
        }
    }
}
