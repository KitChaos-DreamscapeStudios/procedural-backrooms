using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{
    public GameObject Light;
 
 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public override void UseInInventory()
    {
        // throw new System.NotImplementedException();
        
       

        playerStats.GetComponent<Inventory>().HandItem = this;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Light = playerStats.Flashlight;
        Light.GetComponent<FlashlightData>().BatteryLife = 10;


    }
    public override void UseHeld()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        Light = playerStats.Flashlight;
        Light.GetComponent<FlashlightData>().IsOn = !Light.activeSelf;
        Debug.Log("Use");
        Light.SetActive(!Light.activeSelf);
    }
}
