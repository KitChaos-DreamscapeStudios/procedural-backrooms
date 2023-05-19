using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothfection : StatusEffect
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
      
    }
    public override void OnRemove()
    {
        player.GetComponent<Movement3D>().CantMove = true;
        player.GetComponent<Movement3D>().Invoke("RestoreMove", 2);
        player.Health -= 40;
        player.Sanity -= 25;
        EZCameraShake.CameraShaker.Instance.ShakeOnce(10, 10, 0, 0.8f);
        GameObject.Find("MothBurst").GetComponent<ParticleSystem>().Play();
    }
   
}
