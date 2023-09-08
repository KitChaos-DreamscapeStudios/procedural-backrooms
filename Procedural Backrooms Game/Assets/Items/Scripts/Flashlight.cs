using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{
    public GameObject Light;

    public Inventory inventory;
    public Battery AttatchedBattery;
    public bool Active;
    TMPro.TextMeshProUGUI Alert;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Light = playerStats.Flashlight;
        inventory = playerStats.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (inventory.HandItem == null||!inventory.HandItem.GetComponent<Flashlight>())
        {
            Light.SetActive(false);
            Active = false;
            
        }
        if (Active)
        {
            if (AttatchedBattery == null || AttatchedBattery.RemainingCharge <= 0)
            {
                Active = false;
                Light.SetActive(false);
            }
        }
        
        
        if (Active)
        {
            AttatchedBattery.RemainingCharge -= 0.1f * Time.deltaTime;
        }
    }
    public override void Use()
    {
        // throw new System.NotImplementedException();


        GetComponent<AudioSource>().Play();




      
      
        Debug.Log("Use");
        if(AttatchedBattery != null)
        {
            if (AttatchedBattery.RemainingCharge > 0)
            {
                Light.SetActive(!Light.activeSelf);
                Active = !Active;
            }
        }
        else
        {
            var DidGetBat = FindBattery();
            if (DidGetBat)
            {
                Use();
            }
           
        }
       
       




    }
    bool FindBattery()
    {
        foreach (Item item in inventory.Items)
        {
            if (item.GetComponent<Battery>())
            {
                var Bat = item.GetComponent<Battery>();
                if (!Bat.InUse)
                {
                    Bat.InUse = true;
                    AttatchedBattery = Bat;
                    Bat.AttatchedItem = this;
                    return true; 
                }
            }
        }
        return false;
    }
    //public override void UseHeld()
    //{
    //   
    //}
}
