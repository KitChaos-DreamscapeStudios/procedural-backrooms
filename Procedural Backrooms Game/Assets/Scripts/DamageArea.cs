using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float Damage;
    public float Dist;
    PlayerStats player;
    public bool DoSilentDamage;
    public string DeathMessage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < Dist)
        {
            player.TakeDamage(Damage * Time.deltaTime, DeathMessage, DoSilentDamage);
            Debug.Log("DealDmg");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, Dist);
    }
}
