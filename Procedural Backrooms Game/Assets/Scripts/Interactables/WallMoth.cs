using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoth : Interactable
{
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerStats.Thirst += 5;
        playerStats.Sanity -= 8;
        playerStats.Hunger += 15;
        Destroy(gameObject);
        
    }
}
