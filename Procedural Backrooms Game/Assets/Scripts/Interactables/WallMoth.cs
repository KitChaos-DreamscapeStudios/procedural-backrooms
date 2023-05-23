using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoth : Interactable
{
    public PlayerStats playerStats;
    public Mothfection moths;
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
        playerStats.Sanity -= 2;
        playerStats.Hunger += 15;
        if(Random.Range(0, 100) > 90)
        {
            moths = new Mothfection();
            moths.player = playerStats;
            moths.TimeLeft = Random.Range(80, 400);
            playerStats.statuses.Add(moths);
        }
        Destroy(gameObject);
        
    }
}
