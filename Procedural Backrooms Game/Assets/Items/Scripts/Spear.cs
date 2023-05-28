using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Item
{
    public GameObject Projectile;
    public InventorySlot slotIn;
   
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void UseInInventory()
    {
        if (playerStats.GetComponent<Inventory>().HandItem = this)
        {
            playerStats.GetComponent<Inventory>().HandItem = null;
        }
        playerStats.GetComponent<Inventory>().HandItem = this;
    }
    public override void UseHeld()
    {

        var DirectObject = GameObject.Find("Main Camera");
        
        var NewObj = Instantiate(Projectile, DirectObject.transform.position + DirectObject.transform.forward*4, Quaternion.identity);
        NewObj.transform.eulerAngles = DirectObject.transform.eulerAngles;
        NewObj.GetComponent<Rigidbody>().velocity = (DirectObject.transform.forward*65);
        //slotIn.heldItem = null;
        playerStats.GetComponent<Inventory>().HandItem = null;
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);

    }
}
