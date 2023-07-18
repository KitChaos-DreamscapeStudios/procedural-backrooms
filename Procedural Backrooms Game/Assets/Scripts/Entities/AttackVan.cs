using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackVan : Damager
{
    public PlayerStats Player;
    [SerializeField]float Wait;
    float Rand;
    [SerializeField]bool IsActive;
    public AudioSource Drive;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerStats>();
        Drive = GetComponent<AudioSource>();
        Rand = Random.Range(40, 80);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!IsActive)
        {
            Wait += Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, Player.transform.position) < 1)
        {
            Player.TakeDamage(Damage, DeathMessage);
            OnDamage();
        }
       
        if(Wait > Rand && Player.Sanity < 30)
        {
            if (!IsActive)
            {
                TrySetAttackPos();
            }
            if (!Drive.isPlaying &&!IsActive)
            {
                Drive.Play();

            }
            else if (!Drive.isPlaying)
            {
                IsActive = false;
                Wait = 0;
                Rand = Random.Range(40, 70);
            }
            IsActive = true;
            gameObject.MoveForward(1.5f);

           
            
            
        }
        else
        {
            IsActive = false;
        }
        if (!IsActive)
        {
            transform.position = new Vector3(0, 100, 0);
        }
    }
    void TrySetAttackPos()
    {

        RaycastHit hit = new RaycastHit();
        Vector3 targ = Player.transform.position;


        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        
        targ.z = targ.z - objectPos.z;
        var NewPos= targ + Random.onUnitSphere * 30;
        NewPos.y = targ.y-2;
        transform.position = NewPos;
        transform.LookAt(Player.transform);

        if (Physics.Raycast(transform.position, targ, out hit))
        {
            if(!hit.collider.gameObject == Player)
            {
                Wait = 0;
                Rand = Random.Range(30, 70);
            }
            else
            {

            }

        }
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
    public override void OnDamage()
    {
        Player.Sanity -= 20;
        IsActive = false;
        Drive.Stop();
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
