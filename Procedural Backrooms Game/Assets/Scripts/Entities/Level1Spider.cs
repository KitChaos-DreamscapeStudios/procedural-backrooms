using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.AI;
public class Level1Spider : Damager
{
    public enum State
    {
        chasing, //Chasing the player outside of the ceiling
        wandering, //Wandering around, outside of the ceiling
        hunting,  //Moving towards the player, inside the ceiling

    }
    public NavMeshAgent agent;
    public State state;
    public GameObject Player;
    [SerializeField] float elap;
    public bool PlayerDetected;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }
    public override void OnDamage()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90, transform.rotation.y, 0);
        elap += Time.deltaTime;
        if (Vector3.Distance(transform.position, Player.transform.position) < Player.GetComponent<Movement3D>().SoundLevel)
        {
            //Debug.Log("PlayerNear");
            PlayerDetected = true;




        }
        else
        {
            PlayerDetected = false;
        }
        //State Management
        if(PlayerDetected == true && state == State.wandering)
        {
            if(Vector3.Distance(transform.position, Player.transform.position) < 15)
            {
                if(elap >= 5)
                {
                    state = State.chasing;
                }
               
            }
            else
            {
                state = State.hunting;
            }
        }
        if(state == State.hunting)
        {
            GetComponent<MeshRenderer>().enabled = false;
            if (PlayerDetected)
            {
                agent.SetDestination(Player.transform.position);
            }
            if (Vector3.Distance(transform.position, Player.transform.position) < 15)
            {
                if (elap >= 5)
                {
                    state = State.chasing;
                }

            }
        }
        if(Vector3.Distance(transform.position, agent.destination) < 5)
        {
            if(state == State.hunting && !PlayerDetected)
            {
                state = State.wandering;
            }
        }
        if(state == State.chasing)
        {
            GetComponent<MeshRenderer>().enabled = true;
            agent.speed = 15;
            if(Vector3.Distance(transform.position, agent.destination) < 5)
            {
                var rand = Random.Range(0, maxInclusive: 4);
                if(rand == 3)
                {
                    state = State.hunting;
                }
                else
                {
                    agent.SetDestination(Player.transform.position);
                }
            }
        }
        if (state == State.wandering)
        {
            GetComponent<MeshRenderer>().enabled = true;
            if (Vector3.Distance(transform.position, agent.destination) < 5)
            {
                var NewDest = Random.insideUnitSphere * 20;
                NewDest.y = transform.position.y;
                agent.SetDestination(transform.position + NewDest);
            }
            agent.speed = 4.5f;
            if (PlayerDetected)
            {
                state = State.hunting;
            }
        }

    }
}
