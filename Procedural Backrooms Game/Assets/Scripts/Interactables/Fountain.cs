using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : Interactable
{
    public PlayerStats playerStats;
    public float Thirst = 5;
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
        playerStats.Thirst += Thirst;
        //Destroy(gameObject);
    }
}
