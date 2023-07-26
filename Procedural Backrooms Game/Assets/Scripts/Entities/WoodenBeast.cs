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


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Creaker = GetComponent<AudioSource>();
        //BehindWoods = new List<GameObject>(ContainedWood);
    }

    Vector3 SetTarget()
    {
        Orient.LookAt(Player.transform);

        if(Physics.Raycast(Orient.transform.position, Orient.forward, layerMask:3, maxDistance: 10))
        {
            
            LockedOnToPlayer = true;
            return Player.transform.position;
        }
        else
        {
            if(Vector3.Distance(Orient.transform.position, Player.transform.position) < 3)
            {
                Debug.Log("ShouldHurstPlayer");
                Player.GetComponent<PlayerStats>().TakeDamage(15, DeathMessage);
                return Player.transform.position;
            }
            LockedOnToPlayer = false;
            return Orient.transform.position + (Orient.transform.forward * 5);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerStats>().Sanity > 100)
        {
            state = State.hunting;
        }
        if (state != State.inactive)
        {
           
            if (!IsVis)
            {
                WaitToMoveTarg += Time.deltaTime;
                awaitWoodMove += Time.deltaTime;
            }
            if (awaitWoodMove > 0.8f && MovedWood.Count < ContainedWood.Count)
            {
                var moveWood = ContainedWood[Random.Range(0, ContainedWood.Count)];
                if (!MovedWood.Contains(moveWood))
                {
                    MovedWood.Add(moveWood);
                    MoveWood(moveWood);
                }
                
            }
            
            if (WaitToMoveTarg> 8)
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
        Creaker.pitch = Random.Range(-1.5f, 1.5f);
        Creaker.PlayOneShot(Creaks[Random.Range(0, Creaks.Count)]);
       
      wood.transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), Random.Range(-45, 45));
        wood.transform.position = Target + new Vector3(Random.Range(-1.8f,1.8f), 0, Random.Range(-1.8f, 1.8f));
        Orient.transform.position = wood.transform.position;
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
