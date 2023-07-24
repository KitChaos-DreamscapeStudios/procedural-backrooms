using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungalGrabable : Interactable
{
    FungalOrb Orb;
    // Start is called before the first frame update
    void Start()
    {
        Orb = GetComponent<FungalOrb>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract()
    {
        Instantiate(Orb.GlowingThing, transform.position, Quaternion.identity);
        Orb.Wait += 20;
    }
}
