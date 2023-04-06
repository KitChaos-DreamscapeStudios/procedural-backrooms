using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;
    public Item HandItem;
    public GameObject InventScreen;
    public bool InventUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            InventScreen.SetActive(!InventScreen.activeSelf);
        }
        if (Input.GetMouseButtonUp(1) && HandItem!= null&& HandItem.isHoldable)
        {
            HandItem.UseHeld();
        }
        
    }
}
public abstract class Item : MonoBehaviour
{
    public bool isHoldable;
    public bool isUsable;
    public GameObject DropObject;
    public abstract void UseInInventory();
    public abstract void UseHeld();
}
