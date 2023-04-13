using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Hound : Damager
{
    public enum State
    {
        wandering,
        preparing,
        charging,
        hunting,
        searching
    }
    bool WasVis;
    // State Machine For Hounds:
    //   If Wandering, move randomly, growl at random intervals
    //    When a player is seen, change to preparing, growl a lot, and wait up to 5 seconds (Random 1.5-5). Check if the player is making Eye contact, if so extend by 3 seconds (Flat)
    //    Charge Straight towards the player, Moving Quickly, and only re-assigning Nav Target after charge is done
    //    Listen for noise, if the player is in the area and running, or near and walking, enter hunting mode, checking again every 5 or so seconds
    //    If at any point the hound does not detect the player, return to wandering mode.
    //
    //
    //
    //
    //
    //
    //
    //
    //
    public State state;
    public NavMeshAgent agent;
    public GameObject Player;
    [SerializeField]float elap;
    public bool PlayerDetected;
    public float Preptime;
    public float Rand;
    public float GrowlTimer;
    public GameObject Growl;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }
    public override void OnDamage()
    {
        PlayGrowl();
      //  throw new System.NotImplementedException();
    }
    // Update is called once per frame
    void Update()
    {
        elap += Time.deltaTime;
        //Detect State 
        if(Vector3.Distance(transform.position, Player.transform.position) < Player.GetComponent<Movement3D>().SoundLevel)
        {
            //Debug.Log("PlayerNear");
            PlayerDetected = true;
            if ( state != State.charging && state != State.preparing && state != State.hunting)
            {
                state = State.searching;
            }
           
        }
        else
        {
            PlayerDetected = false;
        }
        var Hit = new RaycastHit();
        if(Physics.Raycast(origin: transform.position + transform.forward, direction: transform.forward, out Hit) && state != State.preparing && state != State.hunting && state != State.charging)
        {
            if(Hit.collider.gameObject.layer == Player.layer)
            {
                PlayGrowl();
                //elap = 0;
                state = State.preparing;
               
                agent.SetDestination(transform.position);
            }
           
        }
        GrowlTimer += Time.deltaTime;
        if(state == State.preparing)
        {
           
            
           // elap += Time.deltaTime;

           
            if(elap > 2)
            {
                
            }
            if(elap > 3)
            {
                PlayGrowl();
                elap = 0;
                agent.SetDestination(Player.transform.position);
                state = State.charging;
            }
        }
        if(state == State.charging)
        {
            //elap += Time.deltaTime;
            agent.speed = 50;
            if(Vector3.Distance(transform.position, agent.destination) < 2)
            {
                agent.SetDestination(Player.transform.position);
                state = State.hunting;
            }

        }
        if(state == State.hunting)
        {
            if(GrowlTimer > Rand-2)
            {
                Rand = Random.Range(2, 5);
                PlayGrowl();
                GrowlTimer = 0;
            }
            agent.speed = 5;
           // elap += Time.deltaTime;
            if(elap > 3 || Vector3.Distance(transform.position, agent.destination) < 2)
            {
                
                if(PlayerDetected == false)
                {
                    state = State.wandering;
                    elap = 0;
                }
                else
                {
                    agent.SetDestination(Player.transform.position);
                    elap = 0;
                }
               
            }
        }
        if(state == State.searching)
        {
            if (GrowlTimer > Rand-1)
            {
                Rand = Random.Range(2, 5);
                PlayGrowl();
                GrowlTimer = 0;
            }
            agent.speed = 3.5f;
           // elap += Time.deltaTime;
            if (elap >= 5 || Vector3.Distance(transform.position, agent.destination) < 2)
            {
               
                if (PlayerDetected == false)
                {
                    state = State.wandering;
                    elap = 0;
                }
                else
                {
                    agent.SetDestination(Player.transform.position);
                     elap = 0;
                }
            }
        }
        if(state == State.wandering)
        {
            if (GrowlTimer > Rand)
            {
                Rand = Random.Range(2, 5);
                PlayGrowl();
                GrowlTimer = 0;
            }
            agent.speed = 1.5f;
           // elap += Time.deltaTime;
            if (elap >= 10)
            {
                elap = 0;
                if (PlayerDetected == false)
                {
                    var newDest = Random.insideUnitSphere*20;
                    newDest.y = 0;
                    agent.SetDestination(transform.position + newDest);
                }
                else
                {
                    state = State.hunting;
                    agent.SetDestination(Player.transform.position);
                }
            }
        }
        
    }
    void PlayGrowl()
    {
        var g = Instantiate(Growl, transform.position, Quaternion.identity);
        g.GetComponent<AudioSource>().pitch = Random.Range(-2, 2);
        g.GetComponent<AudioSource>().Play();
        Destroy(g, 5);
    }
    private void OnBecameVisible()
    {
        WasVis = true;
    }
    private void OnBecameInvisible()
    {
        if (WasVis == true && state == State.preparing)
        {
            WasVis = false;
            state = State.charging;
        }
    }
}

