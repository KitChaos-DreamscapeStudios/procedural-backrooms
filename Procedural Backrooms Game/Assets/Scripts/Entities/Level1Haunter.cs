using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Level1Haunter : Damager
{
    public GameObject Player;
    public bool IsInCiel;
    [SerializeField] float NearLightDist;
    Vector3 UpPoint;
    bool SetPointCorrect;
    bool IsRunning;
    Vector3 Target;
    //public Animator anim;
    //Make Enter and exit noises later
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Player = GameObject.Find("Player");
        Target = transform.position + new Vector3(0.1f, 0 ,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(180, transform.eulerAngles.y, transform.eulerAngles.z);
        if (!IsRunning)
        {
            var PlayerSounds = Player.GetComponent<Movement3D>();
            if (PlayerSounds.SoundLevel < Vector3.Distance(transform.position, Player.transform.position))
            {
                HasHeardPlayer = true;
            }
            else
            {
                HasHeardPlayer = false;
            }
            if (HasHeardPlayer)
            {
                Target = Player.transform.position;
            }
            if (Vector3.Distance(transform.position, Target) < 5)
            {
                SetNewTarget();
            }
           
           
            Vector3 targ = Player.transform.position;


            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            targ.z = targ.z - objectPos.z;
            transform.forward = Vector3.Slerp(transform.forward, targ, 0.05f);

           //Add Delibarate attacks later
            if (Vector3.Distance(transform.position, Player.transform.position) <= 0.5f && !IsInCiel)
            {
                Player.GetComponent<PlayerStats>().TakeDamage(0.5f, "Killed By Phage Spider");
            }
            //transform.position = Vector3.Slerp(transform.position, UpPoint,0.08f);
        }
        if (Target != transform.position) 
        {
            if (IsRunning)
            {
                transform.position += transform.forward.normalized / 4;
            }
            else
            {
                transform.position += transform.forward.normalized / 6;
            }
            
        }
        float Lights = CheckLights();
        //make this better later
        if (Lights >= 1)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;

        }
        if (IsRunning)
        {
            if (!IsInvoking("SetNewTarget"))
            {
                Invoke("SetNewTarget", 20);
            }
        }

    }
    void SetNewTarget()
    {
        var Rand = Random.Range(0, 100);
        if(Rand > 70)
        {
            Target = transform.position;
        }
        else
        {
            Target = transform.position + Random.insideUnitSphere * 40;
            Target.y = transform.position.y;
        }
    }
    float CheckLights()
    {
        List<GameObject> Lights;
        Lights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Lights"));
        List<GameObject> NearLights = new List<GameObject>();
        foreach(GameObject L in Lights)
        {
            if(Vector3.Distance(transform.position, L.transform.position) <= NearLightDist)
            {
                NearLights.Add(L);
            }
        }
        return NearLights.Count;

    }
    public override void OnDamage()
    {
        //Hiss.Play();
    }
    public override void OnTakeDamage()
    {
        
    }
    public override void Die()
    {
        Player.GetComponent<PlayerStats>().Score += 90;
        Destroy(gameObject);
    }
}
