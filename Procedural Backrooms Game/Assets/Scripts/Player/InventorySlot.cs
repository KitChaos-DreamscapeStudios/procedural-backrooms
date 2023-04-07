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
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(heldItem != null && button != null)
        {
            //button.onClick.RemoveAllListeners();
           // button.onClick.AddListener(UseItem);
        }
        //button.onClick.AddListener(UseItem);

        if (inventory.Items.Count > SlotNum)
        {
            heldItem = inventory.Items[SlotNum];
        }
        if (inventory.InventScreen.activeSelf)
        {
            if(inventory.Items.Count > SlotNum)
            {
                Icon.sprite = heldItem.Icon;
            }
            if (heldItem == null)
            {
                if (Icon != null)
                {
                    Icon.enabled = false;
                }

            }
            else
            {
                Icon.enabled = true;
            }
        }
    }
    public void OnMouseUpAsButton()
    {
        Test();
    }
    public void Test()
    {
        Debug.Log("Test");
    }

    public void ActivateMenu()
    {
        Menu.SetActive(!Menu.activeInHierarchy);
        
    }
    public void UseItem()
    {
        if(heldItem != null)
        {
            if (heldItem.isUsable)
            {
                heldItem.UseInInventory();
            }
        }
        
    }
    
}
