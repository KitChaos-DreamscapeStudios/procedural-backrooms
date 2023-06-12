using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chips : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void UseHeld()
    {
        throw new System.NotImplementedException();
    }
    public override void UseInInventory()
    {
        playerStats.Thirst -= 5;
        playerStats.Hunger += 10;
        playerStats.Sanity += 3;
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);

    }
}
