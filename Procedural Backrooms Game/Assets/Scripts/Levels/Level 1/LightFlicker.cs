using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public ParticleSystem Burst;
    public AudioSource FlickNoise;
    public AudioSource BurstNoise;
    public float t;
    bool CanBurst;
    float BurstTime;
    public Light L;
    bool IsDoFlick;
    float BaseIntenisty;
    // Start is called before the first frame update
    void Start()
    {
        Burst = GetComponentInChildren<ParticleSystem>();
        FlickNoise = GetComponent<AudioSource>();
       // BurstNoise = GetComponentInChildren<AudioSource>();
        L = GetComponent<Light>();
        BaseIntenisty = L.intensity;
      
        var Rand = Random.Range(0, 5);
        if(Rand == 3)
        {
            CanBurst = true;
        }
        BurstTime = Random.Range(25, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanBurst)
        {
            t += Time.deltaTime;
            if(t - Mathf.Round(t) < 0.05 && CanBurst)
            {
                //BurstNoise.Play();
                var Rand = Random.Range(0, 30);
                if(Rand == 5)
                {
                    
                    FlickNoise.Play();
                    IsDoFlick = true;
                }
                else
                {
                    FlickNoise.Stop();
                    IsDoFlick = false;
                }
            }
            if (t > BurstTime)
            {
                if(Physics.CheckSphere(transform.position, 0.5f, GameObject.Find("Col").layer))
                {
                   // GameObject.Find("Player").GetComponent<PlayerStats>().TakeDamage(20, "Killed by Exploding Light");
                }
                L.intensity = BaseIntenisty;
                BurstNoise.volume = 1;
                BurstNoise.Play();
                CanBurst = false;

                Burst.Play();
                L.intensity = 0;
                Destroy(GetComponent<Light>());
                Destroy(GetComponent<MeshRenderer>());
                // Destroy(FlickNoise);
                Destroy(gameObject, 0.5f);

            }
        }
        
        
        if (IsDoFlick && CanBurst)
        {
            L.intensity = Mathf.Sin(t);
        }
        else
        {
            if (CanBurst)
            {
                L.intensity = BaseIntenisty;
            }
           
        }
    }
}
