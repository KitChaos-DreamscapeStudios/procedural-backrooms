using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decipherable : Item
{
    //public string TryText;
    public string StartReading;
    public string SuccessText;
    public string FailText;
    public bool HasWon;
    public float WinChance;
    public float TryCooldown;
    public float TotalCooldown;
    public bool WinOnce;
    public bool IsBeingRead;
    public string HasWonKey;
    // Start is called before the first frame update
    public void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
       
    }

    // Update is called once per frame
    public void Update()
    {
        if(playerStats.GetComponent<Inventory>().HandItem == this && !playerStats.gameObject.GetComponent<Movement3D>().sprinting)
        {
            if (IsBeingRead)
            {
                TryCooldown += Time.deltaTime;
                if(playerStats.gameObject.GetComponent<Movement3D>().FB != 0)
                {
                    TryCooldown -= Time.deltaTime / 2;
                }
            }
           if(TryCooldown >= TotalCooldown)
           {
               
                float Rand = Random.Range(0, 100);
                
                    if (Rand < WinChance)
                    {
                        SuccessEffect();
                        playerStats.PingAlert(SuccessText, Color.green);
                        HasWon = true;
                    }
                    else
                    {
                        FailEffect();
                    }
                
           }
        }
        else
        {
            IsBeingRead = false;
        }
        if (!playerStats.GetComponent<Inventory>().HasReadScripts.ContainsKey(HasWonKey))
        {
            playerStats.GetComponent<Inventory>().HasReadScripts[HasWonKey] = false;
        }
        else
        {
            if (playerStats.GetComponent<Inventory>().HasReadScripts[HasWonKey])
            {
                HasWon = true;
            }
        }
        
        if (HasWon&&!WinOnce)
        {
            SuccessEffect();
        }
        
    }
    public override void Use()
    {
        playerStats.PingAlert(StartReading, Color.white);
        IsBeingRead = true;
        
    }
    public abstract void SuccessEffect();
    public abstract void FailEffect();
}
