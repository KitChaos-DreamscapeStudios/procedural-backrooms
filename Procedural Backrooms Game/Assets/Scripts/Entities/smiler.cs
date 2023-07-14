using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smiler : Damager
{
    public PlayerStats Player;
    public Item FlashLightItem;
    public float Hunger;
    public AudioSource Scream;
    Vector3 Targ;
    public int NearLights;
    public GameObject NearestLight;
    float WaitToEatNormLight;
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
        transform.parent = null;
        Player = GameObject.Find("Player").GetComponent<PlayerStats>();
        
      
    }

    // Update is called once per frame
    void Update()
    {
        if(state != State.Charging && state != State.Stalking && Vector3.Distance(transform.position, Player.transform.position)> 550)
        {
            Destroy(gameObject);
        }
        Hunger -= Time.deltaTime;
        int NearLights = gameObject.GetNearObjectsWithTag("Lights", 20, out NearestLight);
        if(Hunger < 80 && state  == State.Wandering)
        {
            SetState(State.Grazing);
        }
        else if(state != State.Grazing)
        {
            SetState(State.Wandering);
        }
        if(state == State.Wandering)
        {
            if(Vector3.Distance(Targ, transform.position) < 1)
            {
                AssignNewPoint();
            }
            Vector3 targ = NearestLight.transform.position;


            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = transform.position.y;
            targ.z = targ.z - objectPos.z;
            if (Vector3.Distance(transform.position, targ) > 2)
            {
                transform.LookAt(targ);
                gameObject.MoveForward(6);

            }

        }

        
        if (state == State.Grazing)
        {
            Vector3 targ = NearestLight.transform.position;


            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = transform.position.y;
            targ.z = targ.z - objectPos.z;
            if(Vector3.Distance(transform.position, targ) > 2)
            {
                transform.LookAt(targ);
                gameObject.MoveForward(6);

            }
            else
            {
                WaitToEatNormLight += Time.deltaTime;
                if(WaitToEatNormLight > 3)
                {
                    WaitToEatNormLight = 0;
                    Debug.Log("Ate!");
                    NearestLight.GetComponent<LightFlicker>().BurstNoise.Play();
                    Destroy(NearestLight);
                   
                    Hunger += 25;
                }

            }

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
        }
        
        
        //Special state change logic here
    }
    void AssignNewPoint()
    {
        var TempPoint = transform.position + Random.onUnitSphere * 15;

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
    
    private void OnBecameInvisible()
    {
        Debug.Log("Woosh");
        if(state == State.Stalking)
        {
            transform.position = Player.transform.position - (Player.transform.forward.normalized * 20);
        }
    }
    public override void OnDamage()
    {
        Destroy(gameObject);
        Player.Sanity -= 5;
        if (Player.GetComponent<Inventory>().Items.Contains(FlashLightItem))
        {
            Player.GetComponent<Inventory>().Items.Remove(FlashLightItem);
            Player.Flashlight.SetActive(false);
            //FlashBreak.play
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
        Player.Score += 200;
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
