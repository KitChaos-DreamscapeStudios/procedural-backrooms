using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fungalInfection : StatusEffect
{
    public GameObject FungalOrb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnRemove()
    {
        for (int i = 0; i < 3; i++)
        {
            var Forb =Instantiate(FungalOrb, player.transform.position + new Vector3(Random.Range(-3, 3), -2, Random.Range(-3, 3)), Quaternion.identity);
            Forb.transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), 0);
        }
        player.Health -= 40;
    }
    public override void TickActivation()
    {
        //Latent Effect;
    }
}
