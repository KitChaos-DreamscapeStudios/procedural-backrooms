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
        player.Health -= 2;
    }
    public override void OnRemove()
    {
       //Nothing
    }
}
