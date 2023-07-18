using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Damager
{
    public PlayerStats Player;
    public AudioSource Howl;
    public float CoolDown;
    float WaitLook;
    bool IsSeen;
    float Rand;
    bool Attacking;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerStats>();
        Howl = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rand = Random.Range(30, 60);
        if (Attacking)
        {
            WaitLook += Time.deltaTime;
            Debug.Log("SEEEN");
        }
        if(WaitLook > 30 && !IsSeen)
        {
            WaitLook = 0;
            Attacking = false;
            CoolDown = 0;
        }
        CoolDown += Time.deltaTime;
        if(Player.Sanity < 35)
        {
            if (CoolDown >= Rand &&!Attacking)
            {
                Attacking = true;
                SetUpCharge();
            }
        }
        if (Attacking)
        {
            transform.LookAt(Player.transform.position);
            if (!Howl.isPlaying)
            {
                Howl.pitch += Random.Range(-3, 3);
                Howl.Play();
            }
            gameObject.MoveForward(2); 
        }
        else
        {
            Howl.Stop();
            transform.position = Player.transform.position + new Vector3(0, 100, 0);
        }
    }
    
    void SetUpCharge()
    {
        transform.position = Player.transform.position + Random.onUnitSphere * 20;
        transform.position = new Vector3(transform.position.x, 3.68f, transform.position.z);
        transform.LookAt(Player.transform.position);

    }
    private void OnBecameVisible()
    {
        IsSeen = true;
    }
    private void OnBecameInvisible()
    {
        IsSeen = false;
    }
    public override void OnDamage()
    {
        Player.Sanity -= 5;
        CoolDown = 0;
        Attacking = false;
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
