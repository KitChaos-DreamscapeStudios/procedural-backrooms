using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class PlayerStats : MonoBehaviour
{
    public List<StatusEffect> statuses;
    public Generation LevelStats;
    public ChromaticAberration InsanityAbber;
    public PostProcessVolume Vol;
    public delegate void Event();
    public List<Event> events;
    public GameObject Flashlight;
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
    public float Fatigue;
    //Bars Themselves;
    public float VisFatigue;
    public Image HealthBar;
    public Image StamBar;
    public Image SanityBar;
    public Image HungerBar;
    public Image ThirstBar;
    public Image FatigueBar;
    //Level Stats
    public float SanityDrain;

    public float StatusTick;
   
    // Start is called before the first frame update
    void Start()
    {
        Vol.profile.TryGetSettings<ChromaticAberration>(out InsanityAbber);
        

    }
   
    // Update is called once per frame
    void Update()
    {
        StatusTick += Time.deltaTime;
        if(StatusTick >= 1)
        {
            StatusTick = 0;
            foreach (StatusEffect effect in statuses)
            {
                effect.TimeLeft -= 1;
                effect.TickActivation();
                if(effect.TimeLeft <= 0)
                {
                    statuses.Remove(effect);
                }
            }
        }
        if(Sanity >= MaxSanity)
        {
            Sanity = MaxSanity;
        }
        HealthBar.fillAmount = Health / MaxHealth;
        StamBar.fillAmount = Stamina / MaxStamina;
        SanityBar.fillAmount = Sanity / MaxSanity;
        HungerBar.fillAmount = Hunger / 100;
        ThirstBar.fillAmount = Thirst / 100;
        FatigueBar.fillAmount = VisFatigue / 10;
        
        Thirst -= (1 * Time.deltaTime) * ThirstDrain;//Add Statuses Laters.
        Hunger -= (1 * Time.deltaTime) * HungerDrain;
        Sanity -= (SanityDrain * Time.deltaTime);
        if(Fatigue >= 10)
        {
            Fatigue = 10;
        }
        
        if(Health < MaxHealth)
        {
            Health += HealthRegen * Time.deltaTime;
            
        }
        if (GetComponent<Movement3D>().sprinting)
        {
            Stamina -= 2 * Time.deltaTime;
        }
        else 
        {

            Fatigue = VisFatigue;
         if (Stamina < MaxStamina)
            {
                if (GetComponent<Movement3D>().FB == 0)
                {
                    Stamina += StamRecoverySpeed * Time.deltaTime;
                }
                else
                {
                    Stamina += 0;
                }

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
        if(Stamina <= Fatigue && GetComponent<Movement3D>().sprinting && Fatigue < 10)
        {
            Stamina = Fatigue + 0.01f;  
            VisFatigue += Time.deltaTime;
        }
        
        if(Stamina <= 0)
        {
            Stamina = 0;
        }
        //Sanity Effects
        if(Sanity > 70)
        {
            InsanityAbber.intensity.Override(0);
        }
        if(Sanity < 70 && Sanity > 55)
        {
            
            InsanityAbber.intensity.Override(1);
         
        }
        if(Sanity < 60 && Sanity > 40)
        {
            HungerDrain = 0.055f;
            ThirstDrain = 0.15f;
            GetComponent<Movement3D>().SpeedBoost = 5;
        }
        if(Sanity < 55)
        {
            GetComponent<Movement3D>().sensitivityX =15;
            GetComponent<Movement3D>().sensitivityY = 15;
            InsanityAbber.intensity.Override(5);
        }
        if(Sanity < 40 && Sanity > 20)
        {
            HungerDrain = 0.1f;
            ThirstDrain = 0.2f;
            GetComponent<Movement3D>().SpeedBoost = 8;
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
