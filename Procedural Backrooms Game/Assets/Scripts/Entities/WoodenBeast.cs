using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBeast : Damager
{
    public AudioSource Growl;
    public enum State
    {
        hunting,
        fleeing,
        inactive
    }
    public State state;
    public List<GameObject> ContainedWood;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(state != State.inactive)
        {







        }
    }
    public override void OnDamage()
    {
        throw new System.NotImplementedException();
    }
    public override void OnTakeDamage()
    {
        throw new System.NotImplementedException();
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
