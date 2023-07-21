using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Item
{
    public GameObject Projectile;
    public InventorySlot slotIn;
    public float Velocity;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Use()
    {
       
        var DirectObject = GameObject.Find("Main Camera");

        var NewObj = Instantiate(Projectile, DirectObject.transform.position + DirectObject.transform.forward * 4, Quaternion.identity);
        NewObj.transform.eulerAngles = DirectObject.transform.eulerAngles;
        NewObj.GetComponent<Rigidbody>().velocity = (DirectObject.transform.forward * Velocity);
        //slotIn.heldItem = null;
        playerStats.GetComponent<Inventory>().HandItem = null;
        
        playerStats.gameObject.GetComponent<Inventory>().Items.Remove(this);
        Destroy(gameObject);
    }
   
}
