using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilthMouth : Damager
{
    public PlayerStats playerStats;
    public StatusEffect effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnDamage()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerStats.Sanity -= 2f;
       // playerStats.TakeDamage(10, "Died of Floor water Poisoning");
        //playerStats.Hunger -= 10;
        effect = new FilthVenom();
        effect.player = playerStats;
        effect.TimeLeft = Random.Range(40, 60);
        playerStats.statuses.Add(effect);
      
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
    public override void OnTakeDamage()
    {
        
    }
}
