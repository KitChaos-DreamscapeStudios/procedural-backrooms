using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungalOrb : Damager
{
    public ParticleSystem SporeBurst;
    public PlayerStats playerStats;
    public fungalInfection effect;
    public GameObject Prefab;
    public GameObject GlowingThing;
   public float Wait;
    float R;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        SporeBurst = GetComponent<ParticleSystem>();
        R = Random.Range(20, 80);
    }

    // Update is called once per frame
    void Update()
    {
        Wait += Time.deltaTime;
       
        if (Wait > R)
        {
            SporeBurst.Play();
            Wait = 0;
            R = Random.Range(20, 80);
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
        Instantiate(GlowingThing, transform.position, Quaternion.identity);
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
        var Rand = Random.Range(0, 100);
        if (other.layer == 3&&Rand>80)
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
}
