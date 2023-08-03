using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smiler : Damager
{
    public PlayerStats Player;
    public Item FlashLightItem;
    public float Hunger;
    //public AudioSource Scream;
    Vector3 Targ;
    public int NumNearLights;
    public GameObject NearestLight;
   [SerializeField] float WaitToEatNormLight;
    AudioSource Screech;
    float Wait;

    public enum State
    {
        Wandering,
        Grazing,
        Stalking,
        Charging
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.Find("Player").GetComponent<PlayerStats>();
        Screech = GetComponent<AudioSource>();
        transform.parent = null;
       
        
      
    }
   public void Activate()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Add Stalking Later
        if(state != State.Charging && state != State.Stalking && Vector3.Distance(transform.position, Player.transform.position)> 550)
        {
            Destroy(gameObject);
        }
        Hunger -= Time.deltaTime;
        int NearLights = gameObject.GetNearObjectsWithTag("Lights", 20, out NearestLight);
        NumNearLights = NearLights;
        if(Hunger < 80 && NearLights > 0)
        {
            if(state == State.Wandering)
            {
                SetState(State.Grazing);
            }
           
        }
        else if(state != State.Stalking && state != State.Charging)
        {
            SetState(State.Wandering);
        }
        if(state == State.Wandering)
        {
            if(Vector3.Distance(Targ, transform.position) < 2)
            {
                AssignNewPoint();
            }
            if (NearestLight)
            {
                if (NearestLight.name == "Flashlight" && Player.Flashlight.activeSelf && Hunger < 80 || NearLights < 2 && Vector3.Distance(transform.position, Player.transform.position) < 80 && Player.Flashlight.activeSelf)
                {
                    SetState(State.Stalking);
                }
                else
                {
                    gameObject.MoveForward(8);
                }
            }
            else
            {
                gameObject.MoveForward(8);
            }



            if (Vector3.Distance(transform.position, Targ) > 1)
            {
                transform.LookAt(Targ);
               // gameObject.MoveForward(6);

            }

        }
        if(state == State.Stalking)
        {
            if(Vector3.Distance(transform.position, Player.transform.position) > 300 || Vector3.Distance(transform.position, Player.transform.position) > 200 && !Player.Flashlight.activeSelf)
            {
                SetState(State.Wandering);
            }
            if((Hunger < 30 || Vector3.Distance(transform.position, Player.transform.position) < 20)&&Player.Flashlight.activeSelf)
            {
                SetState(State.Charging);
            }
            if(Hunger < 20)
            {
                SetState(State.Grazing);
            }
            Vector3 targ = Targ;


            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = transform.position.y;
            targ.z = targ.z - objectPos.z;
            if (Vector3.Distance(transform.position, targ) > 2)
            {
                
                transform.LookAt(targ);
                gameObject.MoveForward(4);

            }
            else
            {
                SetStalkPoint();
            }
        }
        if(state == State.Charging)
        {
            Wait += Time.deltaTime;
            Vector3 targ = Player.transform.position;


            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = transform.position.y;
            targ.z = targ.z - objectPos.z;
            if(Vector3.Distance(transform.position, Player.transform.position) > 300)
            {
                SetState(State.Wandering);
            }
           if(Wait > 0.5f)
            {
                var t = Player.Flashlight.transform.position;
                t.y = transform.position.y;
                transform.LookAt(t);
            }
           
            gameObject.MoveForward(2);
        }

        
        if (state == State.Grazing)
        {
            Targ = NearestLight.transform.position;
            Targ.y = transform.position.y;
            if(Vector3.Distance(transform.position, Targ) > 2)
            {
                transform.LookAt(Targ);
                if(NearestLight.name == "Flashlight" && Player.Flashlight.activeSelf &&Hunger<80 || NearLights < 2 && Vector3.Distance(transform.position, Player.transform.position) < 80 && Player.Flashlight.activeSelf && Hunger<80)
                {
                    SetState(State.Stalking);
                }
                else
                {
                    gameObject.MoveForward(8);
                }
               

            }
            else
            {
                WaitToEatNormLight += Time.deltaTime;
                if(WaitToEatNormLight > 3)
                {
                    WaitToEatNormLight = 0;
                    Debug.Log("Ate!");
                    if (NearestLight.GetComponent<LightFlicker>())
                    {
                        NearestLight.GetComponent<LightFlicker>().BurstNoise.Play();
                        Destroy(NearestLight);
                    }
                   
                   
                    Hunger += 35;
                }

            }

        }
        if (Health <= 0)
        {
            Die();
        }
        if(Hunger <= 0)
        {
            Die();
        }
        if(state != State.Charging)
        {
            Screech.Stop();
        }

       
    }
    public void SetState(State which)
    {
        if(state != which)
        {
            state = which;
            if (which == State.Wandering)
            {

                AssignNewPoint();
            }
            if(which == State.Stalking)
            {
                SetStalkPoint();
            }
            if(which == State.Charging)
            {
                Screech.Play();
            }
        }
        
        
        //Special state change logic here
    }
    public void SetStalkPoint()
    {
        var TempPoint = Player.transform.position + Random.onUnitSphere * 15;

        {
            TempPoint.y = transform.position.y;
        }

        RaycastHit hit;

        if (Physics.Linecast(transform.position, TempPoint, out hit))
        {
            // Debug.Log(hit.collider.name);

            TempPoint = hit.point;

            {
                TempPoint.y = transform.position.y;
            }

            if (Vector3.Distance(TempPoint, transform.position) <= .8f)
            {
                goto Skip;
            }
        }


        Targ = TempPoint;
        goto Jump;
        Skip:;
        Targ = transform.position;
        Jump:;
    }
    void AssignNewPoint()
    {
        var TempPoint = transform.position + Random.onUnitSphere * 50;

        {
            TempPoint.y = transform.position.y;
        }

        RaycastHit hit;

        if (Physics.Linecast(transform.position, TempPoint, out hit))
        {
            // Debug.Log(hit.collider.name);

            TempPoint = hit.point;

            {
                TempPoint.y = transform.position.y;
            }

            if (Vector3.Distance(TempPoint, transform.position) <= .8f)
            {
                goto Skip;
            }
        }


        Targ = TempPoint;
        goto Jump;
        Skip:;
        Targ = transform.position;
        Jump:;
    }
    
    
    public override void OnDamage()
    {
        Destroy(gameObject);
        Player.Sanity -= 5;
        if (Player.GetComponent<Inventory>().HandItem.ItemName == "Flashlight")
        {

            //   NearestLight.GetComponent<LightFlicker>().BurstNoise.Play();
            Destroy(Player.GetComponent<Inventory>().HandItem.gameObject);
            
            Destroy(gameObject);
            Player.Flashlight.SetActive(false);
            Player.GetComponent<Inventory>().HandItem = null;
            //FlashBreak.play
        }
    }
    public override void Die()
    {
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        if(Health <= 0)
        {
            Player.Score += 200;
        }
        
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
