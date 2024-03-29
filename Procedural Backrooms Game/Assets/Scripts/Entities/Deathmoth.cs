    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathmoth : Damager
{
    public bool DetectedPlayer;
    float PlayTimer;
    public GameObject Player;
    public LayerMask NotMonster;
    public Animator anim;
    [SerializeField] bool Visible;
    float PostFlyElap;
    public AudioSource Chitter;
    float ChitterTime;
    int Rand = 3;
    public GameObject Meat;
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
        Player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        Chitter = GetComponent<AudioSource>();
    }
    void PlayChitter(float OvveridePitch = 0)
    {
        Chitter.pitch = 1 + Random.Range(-0.5f, 0.5f);
        if(OvveridePitch != 0)
        {
            Chitter.pitch = OvveridePitch;
        }
        Chitter.Play();
    }
    private void Update()
    {
        ChitterTime += Time.deltaTime;
        if(ChitterTime >= Rand)
        {
            ChitterTime = 0;
            Rand = Random.Range(3, 8);
            PlayChitter();
            
        }
        if(Health <= 0)
        {
            Die();
        }
        var Hit = new RaycastHit();
      
        if(Player.GetComponent<Movement3D>().SoundLevel > (Vector3.Distance(transform.position, Player.transform.position)*3) && state != State.Flying)
        {
            state = State.Prepping;
        }
        if(state == State.Prepping)
        {
            PlayTimer += Time.deltaTime;
            if(PlayTimer >= 3)
            {
                PlayTimer = 0;
                PlayChitter();
                state = State.Flying;
            }
        }
        if(state == State.Flying)
        {
            anim.Play("Flap");
            PostFlyElap += Time.deltaTime;
            gameObject.MoveForward(4);
            Vector3 targ = Player.transform.position;
          

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            targ.z = targ.z - objectPos.z;


            if (ChitterTime >= Rand - 0.5f)
            {
               
                PlayChitter();

            }


            //targ.y = targ.y - objectPos.y;
            PlayTimer += Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, targ, 0.1f);
            if(PostFlyElap >= 4 && Vector3.Distance(transform.position, Player.transform.position)>= 30)
            {
                Destroy(gameObject);
            }
            
        }
    }

    // Update is called once per frame

    public override void Die()
    {
        Player.GetComponent<PlayerStats>().Score += 60;
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Instantiate(Meat, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    public override void OnDamage()
    {
        PlayChitter();
    }
    new public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<DamageSpear>())
        {
            Health -= col.gameObject.GetComponent<DamageSpear>().Damage;
            state = State.Flying;
        }

    }
    private void OnBecameInvisible()
    {
        Visible = false;
    }
    private void OnBecameVisible()
    {
        Visible = true;
    }
    public override void OnTakeDamage()
    {
        PlayChitter(10);
    }
}
