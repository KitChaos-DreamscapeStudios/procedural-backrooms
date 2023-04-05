using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public abstract class Item : MonoBehaviour
{
    public bool isHoldable;
    public bool isUsable;
    public abstract void Use();
}
