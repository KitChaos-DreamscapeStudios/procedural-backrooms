using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Deathmoth : Damager
{
    public bool DetectedPlayer;
    float PlayTimer;
    public GameObject Player;
    public LayerMask NotMonster;
    public Animator anim;
    bool Visible;
    
    //public NavMeshAgent agent;
    public enum State 
    {
        Resting,
        Prepping,
        Flying,
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        //agent.enabled = false;
    }
    private void Update()
    {
        var Hit = new RaycastHit();
        if(!Physics.Linecast(transform.position, Player.transform.position, out Hit, NotMonster) && Vector3.Distance(transform.position, Player.transform.position) < 80)
        {
            DetectedPlayer = true;
        }
        if(DetectedPlayer && Player.GetComponent<Movement3D>().sprinting && state != State.Flying)
        {
            state = State.Prepping;
        }
        if(state == State.Prepping)
        {
            PlayTimer += Time.deltaTime;
            if(PlayTimer >= 3)
            {
               
                PlayTimer = 0;
                state = State.Flying;
            }
        }
        if(state == State.Flying)
        {
            transform.position += transform.forward.normalized/6;
            Vector3 targ = Player.transform.position;


            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            targ.z = targ.z - objectPos.z;





            //targ.y = targ.y - objectPos.y;
            // PlayTimer += Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, targ, 0.04f);
          
          //  GetComponent<BodyDof>

        }
        if (Health <= 0)
        {
            Die();
        }
    }
    void AddPositions()
    {
        //Positions.Add(Player.transform.position);
    }

    // Update is called once per frame

    public override void Die()
    {
        Destroy(gameObject);
    }
    public override void OnDamage()
    {
        
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<DamageSpear>())
        {
            Health -= col.gameObject.GetComponent<DamageSpear>().Damage;
            state = State.Flying;
        }

    }
   
}
