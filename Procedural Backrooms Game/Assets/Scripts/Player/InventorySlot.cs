using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is also used for activating Items;
public class InventorySlot : MonoBehaviour
{
    public Item heldItem;
    public GameObject Menu;
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
