using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class GroundWater : Interactable
{
    public PlayerStats playerStats;
    public DrankFloorWater effect;
    public bool MightBeSafe;
    public override void OnInteract()
    {
        var Rand = 100;
        if (MightBeSafe)
        {
            Rand = Random.Range(0, 100);
        }
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerStats.Thirst += 15;
        playerStats.Sanity -= 5;
        if (Rand > 40)
        {
            playerStats.TakeDamage(10, "Died of Floor water Poisoning", true);
            //playerStats.Hunger -= 10;
            effect = new DrankFloorWater();
            effect.player = playerStats;
            effect.TimeLeft = 20;
            playerStats.statuses.Add(effect);
            
        }
        Destroy(gameObject);
        //Specifically for Level 0 Carpet juice patches


    }
}
