using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public Generation LevelStats;
    //Internal Stats
    public float MaxHealth;
    public float HealthRegen;


    public float MaxSanity;
    public float SanityDrainRes = 1;


    public float ThirstDrain = 1;
    public float ThirstDeath;

    public float HungerDrain = 1;
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
    //Level Stats
    public float SanityDrain;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Sanity >= MaxSanity)
        {
            Sanity = MaxSanity;
        }
        HealthBar.fillAmount = Health / MaxHealth;
        StamBar.fillAmount = Stamina / MaxStamina;
        SanityBar.fillAmount = Sanity / MaxSanity;
        HungerBar.fillAmount = Hunger / 100;
        ThirstBar.fillAmount = Thirst / 100;
        
        Thirst -= (1 * Time.deltaTime) * ThirstDrain;//Add Statuses Laters.
        Hunger -= (1 * Time.deltaTime) * HungerDrain;
        Sanity -= (SanityDrain * Time.deltaTime);

        if(Health < MaxHealth)
        {
            Health += HealthRegen * Time.deltaTime;
            
        }
        if (GetComponent<Movement3D>().sprinting)
        {
            Stamina -= 2 * Time.deltaTime;
        }
        else if(Stamina < MaxStamina)
        {
            if(GetComponent<Movement3D>().FB == 0)
            {
                Stamina += StamRecoverySpeed * Time.deltaTime;
            }
            else
            {
                Stamina += 0;
            }
            
        }
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if(Thirst > 100)
        {
            Thirst = 100;
        }
        if(Hunger > 100)
        {
            Hunger = 100;
        }
        if(Sanity > MaxSanity)
        {
            Sanity = MaxSanity;
        }
        if(Stamina <= 0)
        {
            Stamina = 0;
            GetComponent<Movement3D>().sprinting = false;
        }
        
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Damager>())
        {
            Health -= col.gameObject.GetComponent<Damager>().Damage;
            col.gameObject.GetComponent<Damager>().OnDamage();
            EZCameraShake.CameraShaker.Instance.ShakeOnce(10, 5, 0, 0.5f);
        }
    }
}
