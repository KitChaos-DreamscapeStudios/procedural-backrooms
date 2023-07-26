using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBeast : Damager
{
    public AudioSource Growl;
    public enum State
    {
        hunting,
        frozen,
        inactive
    }
    public State state;
    public List<GameObject> ContainedWood;
    public Vector3 Target;
    GameObject Player;
    bool LockedOnToPlayer;
    public Transform Orient;
    float awaitWoodMove;
    bool IsVis;
    public List<AudioClip> Creaks;
    public AudioSource Creaker;
    public List<GameObject> MovedWood;
    float WaitToMoveTarg;
    bool Chasing;
    float LongElap;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Creaker = GetComponent<AudioSource>();
        //BehindWoods = new List<GameObject>(ContainedWood);
    }
    new private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(4, DeathMessage);
            Debug.Log("ShouldHurstPlayer");
        }
    }
    Vector3 SetTarget()
    {
        Orient.LookAt(Player.transform);
        
        if (Physics.Raycast(Orient.transform.position, Orient.forward, layerMask:3, maxDistance: 4))
        {
            
            LockedOnToPlayer = true;
            return Player.transform.position;
        }
        else
        {
          
            LockedOnToPlayer = false;
            return Orient.transform.position + (Orient.transform.forward * 4);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerStats>().Sanity < 70)
        {
            state = State.hunting;
        }
        LongElap += Time.deltaTime;
        if(LongElap > 180)
        {
            Chasing = !Chasing;
        }
        if (state != State.inactive &&Chasing)
        {
           
            if (!IsVis)
            {
                WaitToMoveTarg += Time.deltaTime;
                awaitWoodMove += Time.deltaTime;
            }
            if (awaitWoodMove > 0.2f && MovedWood.Count < ContainedWood.Count)
            {
                awaitWoodMove = 0;
                WaitToMoveTarg = 0;
                var moveWood = ContainedWood[Random.Range(0, ContainedWood.Count)];
                if (!MovedWood.Contains(moveWood))
                {
                    MovedWood.Add(moveWood);
                    MoveWood(moveWood);
                }
                
            }
            
            if (WaitToMoveTarg> 12)
            {
                MovedWood = new List<GameObject>();
                WaitToMoveTarg = 0;
               Target = SetTarget();
               
            }
          
          



        }
    }
  
    private void OnBecameVisible()
    {
        IsVis = true;
    }
    private void OnBecameInvisible()
    {
        IsVis = false;
    }
    void MoveWood(GameObject wood)
    {
       
        Creaker.PlayOneShot(Creaks[Random.Range(0, Creaks.Count)]);
       
      wood.transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), Random.Range(-45, 45));
        wood.transform.position = Target + new Vector3(Random.Range(-1.8f,1.8f), 0, Random.Range(-1.8f, 1.8f));
        Orient.transform.position = wood.transform.position;
        if (Vector3.Distance(Orient.transform.position, Player.transform.position) < 1)
        {

            Player.GetComponent<PlayerStats>().TakeDamage(15, DeathMessage);
           
        }
    }
    public override void OnDamage()
    {
        //throw new System.NotImplementedException();
    }
    public override void OnTakeDamage()
    {
        //throw new System.NotImplementedException();
    }
    public override void Die()
    {
        //throw new System.NotImplementedException();
    }
}
