using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candies : Item
{
    public override void UseInInventory()
    {
        playerStats.Hunger += 18;
        playerStats.Sanity -= 2;
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);
    }
    public override void UseHeld()
    {
        throw new System.NotImplementedException();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
