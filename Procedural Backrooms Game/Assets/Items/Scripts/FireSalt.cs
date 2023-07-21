using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSalt : DamageSpear
{
    public ParticleSystem Explosion;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Explosion = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if(Vector3.Distance((GetComponent<Rigidbody>().velocity),new Vector3(0, 0, 0)) > 1 && col.gameObject.layer != 3)
        {
            Destroy(gameObject);
            Explosion.Play();
            EZCameraShake.CameraShaker.Instance.ShakeOnce((50 - Vector3.Distance(transform.position, Player.transform.position)), 50 - Vector3.Distance(transform.position, Player.transform.position), 0, 1.1f);
            Player.GetComponent<Movement3D>().SoundLevel = 100;
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Damager>())
        {
            other.GetComponent<Damager>().Health -= 1;
        }
    }
}
