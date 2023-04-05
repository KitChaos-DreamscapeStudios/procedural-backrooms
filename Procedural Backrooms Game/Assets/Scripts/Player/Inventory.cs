using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;
    public Item HandItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1) && HandItem.isHoldable)
        {
            HandItem.UseHeld();
        }   
    }
}
public abstract class Item : MonoBehaviour
{
    public bool isHoldable;
    public bool isUsable;
    public abstract void UseInInventory();
    public abstract void UseHeld();
}
