using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Use()
    {
        playerStats.Health += 5;
        playerStats.Hunger += 25;
        
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);
        Destroy(gameObject);
    }
}
