using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyPopper : Throwable
{
    float ChargeTime;
    public AudioClip UnReadyPop;
    public AudioClip Pop;
    public ParticleSystem Confetti;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChargeTime += Time.deltaTime;
    }
    public override void Use()
    {
        if(ChargeTime > 0.3f)
        {
            ChargeTime = 0;
            Confetti.Play();
            //GetComponent<AudioSource>().clip = Pop;
            //GetComponent<AudioSource>().Play();
            
            base.Use();
            
        }
        
        
    }
}
