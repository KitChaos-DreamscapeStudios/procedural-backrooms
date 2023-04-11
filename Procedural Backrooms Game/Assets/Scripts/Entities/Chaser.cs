using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chaser : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Player;
    float elap;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        elap += Time.deltaTime;
       if(elap > 2)
        {
            agent.SetDestination(Player.transform.position);
        }
        
    }
}
