using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class GroundWater : Interactable
{
    public PlayerStats playerStats;
   
    public override void OnInteract()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerStats.Thirst += 15;
        Destroy(gameObject);
    }
}
