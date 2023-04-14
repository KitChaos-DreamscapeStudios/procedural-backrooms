using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using System.Threading.Tasks;
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
    bool WasVis = true;
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
   // public float Preptime;
    public float Rand;
    public float GrowlTimer;
    public GameObject Growl;
    public LayerMask PlayerMask;
    public bool CanSeePlayer;
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
    public async Task SwitchNav()
    {

    }
    // Update is called once per frame
    void Update()
    {
        elap += Time.deltaTime;
        var Hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out Hit, PlayerMask))
        {
            if (Hit.collider.gameObject.layer == Player.layer)
            {
                CanSeePlayer = true;
                if (state != State.preparing && state != State.hunting && state != State.charging)
                {


                    elap = 0;
                    state = State.preparing;
                }
            }
         

        }
        if (Vector3.Distance(transform.position, Player.transform.position) < Player.GetComponent<Movement3D>().SoundLevel)
        {
            //Debug.Log("PlayerNear");
            PlayerDetected = true;
            
       
           
           
        }
        else
        {
            PlayerDetected = false;
        }
        if (state == State.preparing)
        {
            if(WasVis == false)
            {
                elap = 0;
                state = State.charging;
            }
            if(elap > 8)
            {
                elap = 0;
                state = State.charging;
            }
        }
       
       if(state == State.charging)
       {
           agent.speed = 50;
            agent.SetDestination(Player.transform.position);
            if(elap > 3)
            {
                state = State.hunting;
            }
       }
       if(state == State.hunting)
        {
            if (GrowlTimer > Rand-2)
            {
                Rand = Random.Range(2, 5);
                PlayGrowl();
                GrowlTimer = 0;
            }
            agent.speed = 10;
            agent.SetDestination(Player.transform.position);
            if (PlayerDetected == false)
            {
                state = State.wandering;
            }
        }
        if (state == State.wandering)
        {
            agent.speed = 4.5f;
            if (PlayerDetected)
            {
                state = State.searching;
            }
        }
        if(state == State.searching)
        {
            if (PlayerDetected)
            {
                agent.SetDestination(Player.transform.position);
            }
            //Growling
            if (GrowlTimer > Rand)
            {
                Rand = Random.Range(2, 5);
                PlayGrowl();
                GrowlTimer = 0;
            }
            if (Vector3.Distance(transform.position, agent.destination) < 3)
            {

                
                if (!PlayerDetected)
                {
                    var newDest = Random.insideUnitSphere * 20;
                    newDest.y = 0;
                    agent.SetDestination(transform.position + newDest);
                    state = State.wandering;
                }
            }
        }
        if (state == State.wandering)
        {
            //Growling
            if (GrowlTimer > Rand)
            {
                Rand = Random.Range(2, 5);
                PlayGrowl();
                GrowlTimer = 0;
            }
            agent.speed = 1.5f;
            // elap += Time.deltaTime;
            if (Vector3.Distance(transform.position, agent.destination) < 3)
            {

                
                var newDest = Random.insideUnitSphere * 20;
                newDest.y = 0;
                agent.SetDestination(transform.position + newDest);
            }
        }
        //var Hit = new RaycastHit();
        //if(Physics.Raycast(origin: transform.position + transform.forward, direction: transform.forward, out Hit) && state != State.preparing && state != State.hunting && state != State.charging)
        //{
        //    if(Hit.collider.gameObject.layer == Player.layer)
        //    {
        //        PlayGrowl();
        //        //elap = 0;
        //        state = State.preparing;

        //        agent.SetDestination(transform.position);
        //    }

        //}
        //GrowlTimer += Time.deltaTime;
        //if(state == State.preparing)
        //{


        //   // elap += Time.deltaTime;


        //    if(elap > 2)
        //    {

        //    }
        //    if(elap > 3)
        //    {
        //        PlayGrowl();
        //        elap = 0;
        //        agent.SetDestination(Player.transform.position);
        //        state = State.charging;
        //    }
        //}
        //if(state == State.charging)
        //{
        //    //elap += Time.deltaTime;
        //    agent.speed = 50;
        //    if(Vector3.Distance(transform.position, agent.destination) < 2)
        //    {
        //        agent.SetDestination(Player.transform.position);
        //        state = State.hunting;
        //    }

        //}
        //if(state == State.hunting)
        //{
        //    if(GrowlTimer > Rand-2)
        //    {
        //        Rand = Random.Range(2, 5);
        //        PlayGrowl();
        //        GrowlTimer = 0;
        //    }
        //    agent.speed = 5;
        //   // elap += Time.deltaTime;
        //    if(elap > 3 || Vector3.Distance(transform.position, agent.destination) < 2)
        //    {

        //        if(PlayerDetected == false)
        //        {
        //            state = State.wandering;
        //            elap = 0;
        //        }
        //        else
        //        {
        //            agent.SetDestination(Player.transform.position);
        //            elap = 0;
        //        }

        //    }
        //}
        //if(state == State.searching)
        //{
        //    if (GrowlTimer > Rand-1)
        //    {
        //        Rand = Random.Range(2, 5);
        //        PlayGrowl();
        //        GrowlTimer = 0;
        //    }
        //    agent.speed = 3.5f;
        //   // elap += Time.deltaTime;
        //    if (elap >= 5 || Vector3.Distance(transform.position, agent.destination) < 2)
        //    {

        //        if (PlayerDetected == false)
        //        {
        //            state = State.wandering;
        //            elap = 0;
        //        }
        //        else
        //        {
        //            agent.SetDestination(Player.transform.position);
        //             elap = 0;
        //        }
        //    }
        //}


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
        WasVis = false;
    }
}

