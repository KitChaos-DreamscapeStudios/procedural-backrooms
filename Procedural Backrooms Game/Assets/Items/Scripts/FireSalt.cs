using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSalt : DamageSpear
{
    public ParticleSystem Explosion;
    GameObject Player;
    public bool IsItem;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        //Explosion = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if(Vector3.Distance((GetComponent<Rigidbody>().velocity),new Vector3(0, 0, 0)) > 10 && col.gameObject.layer != 3)
        {
           
            Explosion.transform.parent = null;
            Explosion.Play();
            EZCameraShake.CameraShaker.Instance.ShakeOnce((50 - Vector3.Distance(transform.position, Player.transform.position)), 50 - Vector3.Distance(transform.position, Player.transform.position), 0, 1.1f);
            Player.GetComponent<Movement3D>().SoundLevel = 100;
            Destroy(gameObject);
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Damager>())
        {
            other.GetComponent<Damager>().Health -= 1;
        }
        if (other.GetComponent<PlayerStats>())
        {
            other.GetComponent<PlayerStats>().TakeDamage(1, "Blown up by Firesalt");
        }
    }
}
