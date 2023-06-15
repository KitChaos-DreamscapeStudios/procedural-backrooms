using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairwell : Interactable
{
    bool StartMove;
    GameObject Player;
    public Vector3 TargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(StartMove && !Player.GetComponent<PlayerStats>().IsSleeping)
        {
            Player.transform.position = transform.position + TargetPosition;
            StartMove = false;
        }
    }
    public override void OnInteract()
    {
        Player.GetComponent<PlayerStats>().IsSleeping = true;
        StartMove = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + TargetPosition, .5f  );
    }
}
