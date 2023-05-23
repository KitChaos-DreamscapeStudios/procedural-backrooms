using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
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


    float SleepButtonTimer;
    public float BedQuality;
    public Image SleepShade;
    public bool IsSleeping;
    // Start is called before the first frame update
    void Start()
    {
        Vol.profile.TryGetSettings<ChromaticAberration>(out InsanityAbber);
        

    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            SleepButtonTimer += Time.deltaTime;
        }
        else
        {
            SleepButtonTimer = 0;
        }
        if(SleepButtonTimer > 3)
        {
            Sleep();
            SleepButtonTimer = 0;
        }
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
                    effect.OnRemove();
                    statuses.Remove(effect);
                }
            }
        }
        if(Sanity >= MaxSanity)
        {
            Sanity = MaxSanity;
        }
        if (IsSleeping)
        {
            SleepShade.color = Color.Lerp(SleepShade.color, new Color(0, 0, 0, 1), 0.06f);
            if(SleepShade.color == new Color(0, 0, 0, 1))
            {
                IsSleeping = false;
            }
        }
        else
        {
            if(SleepShade.color != new Color(0, 0, 0, 0))
            {
                SleepShade.color = Color.Lerp(SleepShade.color, new Color(0, 0, 0, 0), 0.06f);
            }
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
            HungerDrain = 0.1f;
            ThirstDrain = 0.2f;
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
            HungerDrain = 0.2f;
            ThirstDrain = 0.4f;
            GetComponent<Movement3D>().SpeedBoost = 8;
            //Make sure to add creepy ambience later
        }
        
        if(Thirst <= ThirstDeath)
        {
            Die("Died of Thirst");
        }   
        if(Hunger <= HungerDeath)
        {
            Die("Starved to Death");
        }
        if(Fatigue < 0 || VisFatigue < 0)
        {
            Fatigue = 0;
            VisFatigue = 0;
        }
    }
   public  void Sleep(float OverrideQuality = 0)
    {
        if(OverrideQuality != 0)
        {
            VisFatigue -= OverrideQuality; //Make sure to set visfatigue anytime you mess with fatigue

            Sanity += OverrideQuality * 10 / SanityDrain + 1;
        }
        IsSleeping = true;
        Hunger /= 1.3f;
        Thirst /= 1.5f;

        VisFatigue -= BedQuality; //Make sure to set visfatigue anytime you mess with fatigue

        Sanity += BedQuality * 10 / SanityDrain + 1;
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
    void Die(string Reason)
    {//Add some animation for death Later
        var R = Instantiate(new GameObject());
        R.AddComponent<DeathReason>();
        R.GetComponent<DeathReason>().Reason = Reason;

        R.name = "Death Data";
        DontDestroyOnLoad(R);
        SceneManager.LoadScene("Death");
        Destroy(gameObject);
    }
}
public class DeathReason : MonoBehaviour
{
    public string Reason;
    public DeathReason(string reason)
    {
        this.Reason = reason;  
    }
}
