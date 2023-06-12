using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishItem : Item
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
        playerStats.Hunger += 30;
        playerStats.Sanity -= 5;
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);
    }
}
