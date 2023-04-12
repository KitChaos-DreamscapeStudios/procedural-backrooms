using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Hound : MonoBehaviour
{
    public enum state
    {
        wandering,
        preparing,
        charging,
        hunting,
    }

    // State Machine For Hounds:
    //   If Wandering, move randomly, growl at random intervals
    //    When a player is seen, change to preparing, growl a lot, and wait up to 5 seconds (Random 1.5-5). Check if the player is making Eye contact, if so extend by 3 seconds (Flat)
    //    Charge Straight towards the player, Moving Quickly, and only re-assigning Nav Target after charge is done
    //    Listen for noise, if the player is in the area and running, or near and walking, enter hunting mode, checking again every 5 or so seconds
    //    If at any point the hound does not detect the player, return to wandering mode.
    //
    //
    //
    //
    //
    //
    //
    //
    //

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
       
        
    }
}
