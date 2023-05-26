using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class GroundWater : Interactable
{
    public PlayerStats playerStats;
    public DrankFloorWater effect;
    public override void OnInteract()
    {
        //Specifically for Level 0 Carpet juice patches
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerStats.Thirst += 15;
        playerStats.Sanity -= 5;
        playerStats.TakeDamage(10, "Died of Floor water Poisoning");
        //playerStats.Hunger -= 10;
        effect = new DrankFloorWater();
        effect.player = playerStats;
        effect.TimeLeft = 20;
        playerStats.statuses.Add(effect);
        Destroy(gameObject);
    }
}
