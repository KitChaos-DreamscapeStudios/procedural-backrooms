using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrankFloorWater : StatusEffect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void TickActivation()
    {
        player.TakeDamage(2, "Died from floor water poisoning");
    }
    public override void OnRemove()
    {
       //Nothing
    }
}
