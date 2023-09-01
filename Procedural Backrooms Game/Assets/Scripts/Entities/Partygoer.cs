using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partygoer : Damager
{
    public AudioSource Vocalizations;
   // public Vector3 EyePoint;
    public List<AudioClip> Sounds;
    public List<AudioClip> AngryNoises;
    public Vector3 Point;
    public Rigidbody body;
    public bool DetectedPlayer;
    public GameObject Player;
    public ParticleSystem Bloons;
    Vector3 UptargPos;
    bool IsActiveWarp;
    public RaycastHit CanSeePlayer;
    public Transform EyeOrient;
    public LayerMask NotMe;
    public Vector3 LastSeenPLayerPoint;
    public float OverrideWaitPoint;
    public enum PartyState
    {
        Wandering,
        Chasing,
        Warping,
        Stunned
    }
    public PartyState State;
    float Warptime;
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Point);
        Gizmos.DrawSphere(Point, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        body = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        //Bloons.transform.position = Player.transform.position - new Vector3(0, 1, 0);
        //EyePoint = transform.position + EyePoint;
       
        Point = transform.position;
        if(Vector3.Distance(transform.position, new Vector3(0,0,0)) < 10)
        {
            Destroy(gameObject);
        }
    }
    //Partygoer AI
    //
    //States
    //Wandering
    //Warping
    //Chasing
    //
    //
    //Starts in Chasing
    //
    //Two Eyes, one looks at the last point it saw the player, another looks directly at the player, checking sightlines.
    //Moves On Eye One, until it reaches last point it saw the player.
    //If eye two still sees the player, continue moving along
    //Otherwise, begin wandering
    //If the player is heard, then change to warping, dissapear into the the ground, and play balloons on the player and begin emergence, entering chase
    //Repeat
    //To allow for better sneaking, the second eye cannot deviate more than 90 degrees from the first eye
    //
    //
    //
    //
    //

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) > 500)
        {
            Destroy(gameObject);
        }
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        if(State != PartyState.Stunned)
        {
            if(State == PartyState.Wandering)
            {
                if (!Vocalizations.isPlaying)
                {
                    Vocalizations.clip = Sounds[Random.Range(0, Sounds.Count)];
                    Vocalizations.Play();
                }
                OverrideWaitPoint += Time.deltaTime;
                if(OverrideWaitPoint >= 45)
                {
                    AssignNewPoint();
                }
                Physics.Raycast(transform.position, direction: EyeOrient.forward, out CanSeePlayer, maxDistance: Mathf.Infinity, layerMask: NotMe);
                if (CanSeePlayer.collider)
                {
                    if (CanSeePlayer.collider.gameObject.layer == 3)
                    {
                        LastSeenPLayerPoint = Player.transform.position;
                        State = PartyState.Chasing;
                    }
                }
                    
                
               
                Vector3 targ = Point;
                //Checkdist = Mathf.Lerp(Checkdist, 2, 0.1f);
                Vector3 objectPos = transform.position;
                targ -= objectPos;
                //var CheckNear = Physics.CheckSphere(transform.position, Checkdist);

                transform.forward = Vector3.Slerp(transform.forward, targ, 0.7f);
                body.velocity = (transform.forward.normalized * 40) / 20;
                if (Vector3.Distance(transform.position, Point) <= 3f)
                {
                   
                    AssignNewPoint();
                }
                if (DetectedPlayer)
                {
                    Warptime = 0;
                    IsActiveWarp = false;
                    State = PartyState.Warping;
                }
            }
            if(State == PartyState.Warping)
            {
                Warptime += Time.deltaTime;
               
                Bloons.Play();
                if (!IsActiveWarp)
                {
                    transform.position = Player.transform.position - new Vector3(0, 20, 0);
                    Bloons.transform.position = Player.transform.position;
                    if (!Vocalizations.isPlaying)
                    {
                        Vocalizations.clip = AngryNoises[Random.Range(0, AngryNoises.Count)];
                        Vocalizations.Play();
                    }
                }
               
                if (Warptime > 3 &&!IsActiveWarp)
                {
                    IsActiveWarp = true;
                    Warptime = 0;
                    
                    UptargPos = Player.transform.position;  
                }
               
                if(Warptime <3 && IsActiveWarp)
                {
                    transform.position = Vector3.Lerp(transform.position, UptargPos, 0.3f);
                }
                else if (IsActiveWarp)
                {
                    Warptime = 0;
                    IsActiveWarp = false;
                    State = PartyState.Chasing;
                }

                
            }
            if(State == PartyState.Chasing)
            {
                Bloons.Stop();
                if (!Vocalizations.isPlaying)
                {
                    Vocalizations.clip = AngryNoises[Random.Range(0, AngryNoises.Count)];
                    Vocalizations.Play();
                }
                Physics.Raycast(transform.position, direction: EyeOrient.forward, out CanSeePlayer, maxDistance: Mathf.Infinity, layerMask: NotMe);
                if(CanSeePlayer.collider.gameObject.layer == 3)
                {
                    LastSeenPLayerPoint = Player.transform.position;
                }
                Vector3 targ = LastSeenPLayerPoint;
                //Checkdist = Mathf.Lerp(Checkdist, 2, 0.1f);
                Vector3 objectPos = transform.position;
                targ -= objectPos;
                //var CheckNear = Physics.CheckSphere(transform.position, Checkdist);

                transform.forward = Vector3.Slerp(transform.forward, targ, 0.8f);
                body.velocity = (transform.forward.normalized * 40)/2;
                if(Vector3.Distance(transform.position, LastSeenPLayerPoint) <= 0.4f)
                {
                    State = PartyState.Wandering;
                }

            }
            EyeOrient.transform.LookAt(Player.transform.position);
        }
    }
    void AssignNewPoint()
    {
        OverrideWaitPoint = 0;
        var TempPoint = transform.position + Random.onUnitSphere * 100;
       
            TempPoint.y = transform.position.y;
        

        RaycastHit hit;

        if (Physics.Linecast(transform.position + transform.forward, TempPoint, out hit))
        {
            // Debug.Log(hit.collider.name);

            TempPoint = hit.point;
           
                TempPoint.y = transform.position.y;


            

            if (Vector3.Distance(TempPoint, transform.position) <= 3f)
            {
                goto Skip;
            }
        }


        Point = TempPoint;
        goto Jump;
        Skip:;
        Point = transform.position;
        Jump:;
    }
    public override void OnDamage()
    {
        //throw new System.NotImplementedException();
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
}
