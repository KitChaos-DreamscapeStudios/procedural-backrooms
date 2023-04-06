using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This is also used for activating Items;
public class InventorySlot : MonoBehaviour
{
    public Item heldItem;
    public GameObject Menu;
    public Image Icon;
    public Inventory inventory;
    public int SlotNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMenu()
    {
        Menu.SetActive(!Menu.activeInHierarchy);
        
    }
    public void UseItem()
    {
        if (heldItem.isUsable)
        {
            heldItem.UseInInventory();
        }
    }
    
}
