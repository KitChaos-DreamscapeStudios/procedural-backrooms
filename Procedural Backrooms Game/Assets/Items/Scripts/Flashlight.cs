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
        if(playerStats.GetComponent<Inventory>().HandItem = this)
        {
            playerStats.GetComponent<Inventory>().HandItem = null;
        }
        playerStats.GetComponent<Inventory>().HandItem = this;
    }
    public override void UseHeld()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Light = playerStats.Flashlight;
        Debug.Log("Use");
       Light.SetActive(!Light.activeSelf);
    }
}
