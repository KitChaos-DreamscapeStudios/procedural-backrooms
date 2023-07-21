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
    public PlayerStats playerStats;
    public TMPro.TextMeshProUGUI label;
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
        if(heldItem != null)
        {
            label.text = heldItem.ItemName;
            DontDestroyOnLoad(heldItem);    
        }
        else
        {
            label.text = "";
        }
        if(inventory.HandItem != heldItem && heldItem != null)
        {
            heldItem.gameObject.transform.position = transform.position + new Vector3(0, 100, 0);
        }
        if (inventory.Items.Count > SlotNum)
        {
            heldItem = inventory.Items[SlotNum];
        }
        else
        {
            heldItem = null;
        }
        if (inventory.InventScreen.activeSelf)
        {
            if(inventory.Items.Count > SlotNum)
            {
                Icon.sprite = heldItem.Icon;
            }
            else
            {
                Icon.sprite = null;
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

    public void DropHeldItem()
    {
        //Menu.SetActive(!Menu.activeInHierarchy);
        if(playerStats.gameObject.GetComponent<Inventory>().HandItem != heldItem)
        {
            Instantiate(heldItem.DropObject, playerStats.gameObject.transform.position, Quaternion.identity);

            playerStats.gameObject.GetComponent<Inventory>().Items.Remove(heldItem);
            Destroy(heldItem.gameObject);
            heldItem = null;

        }
        else
        {
            Destroy(playerStats.GetComponent<Inventory>().HandItem.gameObject);
            playerStats.gameObject.GetComponent<Inventory>().HandItem = null;
           // Destroy(heldItem);
            Instantiate(heldItem.DropObject, playerStats.gameObject.transform.position, Quaternion.identity);

            playerStats.gameObject.GetComponent<Inventory>().Items.Remove(heldItem);
            heldItem = null;
        }
       

    }
    public void UseItem()
    {
        if(heldItem != null)
        {
            inventory.HandItem = heldItem;
            //if (heldItem.isUsable)
            //{
            //    heldItem.playerStats = playerStats;
            //    heldItem.Use();
            //    if (!heldItem.isHoldable)
            //    {
            //        heldItem = null;
            //    }
                
            //}
        }
        
    }
    
}
