using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungalOrb : Damager
{
    public ParticleSystem SporeBurst;
    public PlayerStats playerStats;
    public fungalInfection effect;
    public GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        SporeBurst = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
    public override void OnDamage()
    {
        SporeBurst.Play();
        var Rand = Random.Range(0, 100);
        if(Rand > 20)
        {
            playerStats.Sanity -= 2f;
            // playerStats.TakeDamage(10, "Died of Floor water Poisoning");
            //playerStats.Hunger -= 10;
            effect = new fungalInfection();
            effect.player = playerStats;
            effect.FungalOrb = Prefab;
            effect.TimeLeft = Random.Range(40, 360);
            playerStats.statuses.Add(effect);
        }
       
    }
    public override void OnTakeDamage()
    {
        SporeBurst.Play();

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 3)
        {
            Debug.Log("Gnoncks");
           
        }
    }
}
