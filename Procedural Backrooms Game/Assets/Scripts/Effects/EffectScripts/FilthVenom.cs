using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilthVenom : StatusEffect
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
        player.TakeDamage(0.5f, "Poisoned by Filth Mouth Venom",true);
        player.Sanity -= 0.2f;
    }
    public override void OnRemove()
    {
       // throw new System.NotImplementedException();
    }
}
