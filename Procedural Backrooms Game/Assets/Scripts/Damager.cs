using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damager : MonoBehaviour
{
    public float Damage;
    public float Health;
    public string DeathMessage;
    public bool HasHeardPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<DamageSpear>())
        {
            Health -= col.gameObject.GetComponent<DamageSpear>().Damage;
            OnTakeDamage();
        }
    }
    public abstract void OnTakeDamage();
    public abstract void OnDamage();
    public abstract void Die();
}
