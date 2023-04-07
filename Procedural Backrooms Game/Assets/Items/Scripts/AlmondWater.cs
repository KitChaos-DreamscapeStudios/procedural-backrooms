using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmondWater : Item
{
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void UseHeld()
    {
        Debug.LogError("This Item Does not have a UseHeld!");

    }
    public override void UseInInventory()
    {
        playerStats.Health += 5;
        playerStats.Thirst += 25;
        playerStats.Sanity += 10;
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);
    }
}
