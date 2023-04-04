using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    //Internal Stats
    public float MaxHealth;
    public float HealthRegen;


    public float MaxSanity;
    public float SanityDrainRes;


    public float ThirstDrain;
    public float ThirstDeath;

    public float HungerDrain;
    public float HungerDeath;

    public float MaxStamina;
    public float StamRecoverySpeed;
    //Bar Stats
    public float Health;
    public float Stamina;
    public float Sanity;
    public float Thirst;
    public float Hunger;
    //Bars Themselves;
    public Image HealthBar;
    public Image StamBar;
    public Image SanityBar;
    public Image HungerBar;
    public Image ThirstBar;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = Health / MaxHealth;
        StamBar.fillAmount = Stamina / MaxStamina;
        SanityBar.fillAmount = Sanity / MaxSanity;
        HungerBar.fillAmount = Hunger / 100;
        ThirstBar.fillAmount = Thirst / 100;
        
    }
   
}
