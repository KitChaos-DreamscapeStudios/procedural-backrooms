using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grind : Damager
{
    public AudioSource Hiss;
    GameObject Player;
   // public float Speed;
    Vector3 Target;
    bool HasHeardPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Target = transform.position;
        Player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        var PlayerSounds = Player.GetComponent<Movement3D>();
        if(PlayerSounds.SoundLevel < Vector3.Distance(transform.position, Player.transform.position))
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
            Target = transform.position + Random.insideUnitSphere * 40;
            Target.y = transform.position.y;
        }
        transform.position += transform.forward.normalized / 6;
        Vector3 targ = Player.transform.position;


        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        targ.z = targ.z - objectPos.z;
        transform.forward = Vector3.Slerp(transform.forward, targ, 0.1f);

    }
    public override void Die()
    {
        Destroy(gameObject);
    }
    public override void OnDamage()
    {
        
    }
    void PlayHiss()
    {

        Hiss.pitch = 1 + Random.Range(-0.5f, 0.5f);
        Hiss.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * 2;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<PlayerStats>())
        {
            other.GetComponentInParent<PlayerStats>().TakeDamage(0.1f, "Consumed by The Grind");
        }
    }
}
