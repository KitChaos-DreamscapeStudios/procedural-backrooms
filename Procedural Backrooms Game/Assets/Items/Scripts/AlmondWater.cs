using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmondWater : Item
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public override void Use()
    {
        playerStats.Health += 5;
        playerStats.Thirst += 25;
        playerStats.Sanity += 5;
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);
        Destroy(gameObject);
    }
}
