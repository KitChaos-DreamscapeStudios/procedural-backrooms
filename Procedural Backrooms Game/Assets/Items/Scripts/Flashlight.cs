using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{
    public GameObject Light;
 
 
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Light = playerStats.Flashlight;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public override void Use()
    {
        // throw new System.NotImplementedException();







      
      
        Debug.Log("Use");
        Light.SetActive(!Light.activeSelf);




    }
    //public override void UseHeld()
    //{
    //   
    //}
}
