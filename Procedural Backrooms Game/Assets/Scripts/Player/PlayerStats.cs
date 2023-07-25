using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public string Specialization;
    public float Score;
    public AudioSource Hurt;
    public List<StatusEffect> statuses;
    public Generation LevelStats;
    public ChromaticAberration InsanityAbber;
    public DepthOfField blur;
    public Vignette HurtGlow;
    public PostProcessVolume Vol;
    
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


    public float SleepButtonTimer;
    public float BedQuality;
    public Image SleepShade;
    public bool IsSleeping;

    //Stats for Specializations
    public float ExtraHungerDrain;
    public float ExtraSanityDrain;
    public float ExtraHuntingPoints;
    public float ExtraPoints;
    public bool SleepPrevented;
    public string CantSleepText;
    public List<string> CantSleepReasons;
    // Start is called before the first frame update
    void Start()
    {
        
        Vol.profile.TryGetSettings<ChromaticAberration>(out InsanityAbber);
        Vol.profile.TryGetSettings<DepthOfField>(out blur);
        Vol.profile.TryGetSettings<Vignette>(out HurtGlow);
        

    }
   void GetSpecial()
    {
        Specialization = GameObject.Find("SpecHolder").GetComponent<SpecHolder>().Specialization;
    }
    // Update is called once per frame
    void Update()
    {
        HurtGlow.opacity.Override(100);
        HurtGlow.intensity.Override((100 - Health) / 100);
       // InvokeRepeating("GetSpecial", 0.1f, 0.1f);
        if(Specialization == "Hunter")//Make this code better later
        {
            ExtraHungerDrain = 0.1f;
            ExtraHuntingPoints = 0.3f;
        }
        if(Specialization == "Lost")
        {
            ExtraPoints = 0.1f;
            ExtraSanityDrain = 0.2f;
        }

        if (Input.GetKey(KeyCode.M))
        {
            SleepButtonTimer += Time.deltaTime;
        }
        else
        {
            SleepButtonTimer = 0;
        }
        if(SleepButtonTimer > 3 && !SleepPrevented)
        {
            Sleep();
            SleepButtonTimer = 0;
        }
        else if (SleepPrevented && SleepButtonTimer > 3)
        {
            GetComponent<Inventory>().PickUpTooltip.text = CantSleepText;
        }
        StatusTick += Time.deltaTime;
        List<StatusEffect> RemStat = new List<StatusEffect>();
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
                    RemStat.Add(effect);
                }
            }
        }
        foreach(StatusEffect statusEffect in RemStat)
        {
            statuses.Remove(statusEffect);
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
        
        Thirst -= (1 * Time.deltaTime) * ThirstDrain;
        Hunger -= (1 * Time.deltaTime) * HungerDrain + (1 * Time.deltaTime) * ExtraHungerDrain;
        Sanity -= (SanityDrain * Time.deltaTime) + (SanityDrain*Time.deltaTime)*ExtraSanityDrain;
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
        
        Score += Time.deltaTime + (Time.deltaTime*ExtraPoints);
        //Sanity Effects
        if(Sanity > 70)
        {
            InsanityAbber.intensity.Override(0);
            GetComponent<Movement3D>().SpeedBoost = 0;
            GetComponent<Movement3D>().SpeedBoost = 0;
            GetComponent<Movement3D>().sensitivityX = 5;
            GetComponent<Movement3D>().sensitivityY = 5;
            HungerDrain = 0.1f;
            ThirstDrain = 0.2f;

        }
        if(Sanity < 70 && Sanity > 55)
        {
            
            InsanityAbber.intensity.Override(1);
            GetComponent<Movement3D>().SpeedBoost = 0;
            GetComponent<Movement3D>().sensitivityX = 5;
            GetComponent<Movement3D>().sensitivityY = 5;
        }
        if(Sanity < 60 && Sanity > 40)
        {
           
            GetComponent<Movement3D>().SpeedBoost = 5;
        }
        if(Sanity < 55 && Sanity > 20)
        {
            GetComponent<Movement3D>().sensitivityX =10;
            GetComponent<Movement3D>().sensitivityY = 10;
            InsanityAbber.intensity.Override(5);
        }
       
        if(Sanity < 40 && Sanity > 20)
        {
            HungerDrain = 0.4f;
            ThirstDrain = 0.8f;
            GetComponent<Movement3D>().SpeedBoost = 8;
            //Make sure to add creepy ambience later
        }
        if (Sanity < 35)
        {
            blur.focusDistance.Override(3f);
        }
        else
        {
            blur.focusDistance.Override(4.3f);
        }
        if(Sanity > 20 && Sanity < 55)
        {
            
            GetComponent<Movement3D>().sensitivityX = 15;
            GetComponent<Movement3D>().sensitivityY = 15;
        }
        if (Sanity < 20)
        {
            HungerDrain = 0.8f;
            ThirstDrain = 1f;
            GetComponent<Movement3D>().SpeedBoost = 10;
            GetComponent<Movement3D>().sensitivityX = 20;
            GetComponent<Movement3D>().sensitivityY = 20;
            ThirstBar.color = new Color(0, 0, 0, 0);
            HungerBar.color = new Color(0, 0, 0, 0);
        }
        else
        {
            ThirstBar.color = new Color(0.3607843f, 0.5496334f, 1, 1);
            HungerBar.color = new Color(1, 0.7781968f, 0.3622641f, 1);
        }
        if (Sanity <= 0)
        {
            Sanity = 0;
        }

        if (Thirst <= ThirstDeath)
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
    public void TakeDamage(float DMG, string Type, bool IsSilent = false)
    {
        if(!IsSilent)
        {
            Hurt.Play();
        }
       
        Health -= DMG;
        if(Health <= 0)
        {
            Die(Type);
        }
    }
   public  void Sleep(float OverrideQuality = 0)
    {
        if(OverrideQuality != 0)
        {
            VisFatigue -= OverrideQuality*1.5f; //Make sure to set visfatigue anytime you mess with fatigue

            Sanity += OverrideQuality * 10 / SanityDrain + 1;
        }
        IsSleeping = true;
        Hunger /= 1.3f;
        Thirst /= 1.5f;

        VisFatigue -= BedQuality*5f; //Make sure to set visfatigue anytime you mess with fatigue

        Sanity += BedQuality * 20 / (SanityDrain + 1);
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Damager>())
        {
           
            TakeDamage(col.gameObject.GetComponent<Damager>().Damage, col.gameObject.GetComponent<Damager>().DeathMessage);
            col.gameObject.GetComponent<Damager>().OnDamage();
            EZCameraShake.CameraShaker.Instance.ShakeOnce(50, 15, 0.1f, 0.5f);
        }
    }
    public void TryModSleepCon(string Con, bool Rem = false)
    {
        if (!Rem)
        {
            if (!CantSleepReasons.Contains(Con))
            {
                CantSleepReasons.Add(Con);
            }
        }
        else
        {
            if (CantSleepReasons.Contains(Con))
            {
                CantSleepReasons.Remove(Con);
            }
        }
       
    }
    
    void Die(string Reason)
    {//Add some animation for death Later
        if (IsSleeping)
        {
            Reason += " While Sleeping";
        }
        var R = Instantiate(new GameObject());
        R.AddComponent<DeathReason>();
        R.GetComponent<DeathReason>().Reason = Reason;
        R.GetComponent<DeathReason>().Score = Score;
        R.name = "Death Data";
        DontDestroyOnLoad(R);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Death");
        Destroy(gameObject);
    }
  
}
public class DeathReason : MonoBehaviour
{
    public string Reason;
    public float Score;
    public DeathReason(string reason, float score)
    {
        this.Reason = reason;
        this.Score = score;
    }
}
[System.Serializable]
public struct SanityTier
{
    public float HungerDrain;
    public float ThirstDrain;
    public bool BarsActive;
    public int Sensitivity;
    public float SpeedBoost;
    public float Abberation;
    public SanityTier(float HS, float TS, bool BarsActive, int Sense, float Speed, float Abber)
    {
        this.HungerDrain = HS;
        this.ThirstDrain = HS;
        this.BarsActive = BarsActive;
        this.Sensitivity = Sense;
        this.SpeedBoost = Speed;
        this.Abberation = Abber;
    }
}
