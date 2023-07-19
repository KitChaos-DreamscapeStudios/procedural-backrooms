using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBurst : MonoBehaviour
{
    public ParticleSystem Burst;
    public AudioSource Hiss;
    bool AlwaysActive;
    public AudioSource LoopHiss;
    [SerializeField] Material Hallu;
    [SerializeField] Material Tranq;
    enum GasTypes
    {
        Steam,
        Hallu,
        Tranq
    }
    GasTypes BurstType;
    // Start is called before the first frame update
    void Start()
    {
        Burst = GetComponent<ParticleSystem>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(-80,80));
        var IsAlwaysBurst = Random.Range(0, maxInclusive:100.1f);
        var GasType = Random.Range(0, 100.1f);
        if (GasType > 75 && GasType < 90)
        {
            BurstType = GasTypes.Tranq;
            var M = GetComponent<ParticleSystemRenderer>();
            M.material = Tranq;
        }
        else if (GasType > 90)
        {
            BurstType = GasTypes.Hallu;
            var M = GetComponent<ParticleSystemRenderer>();
            M.material = Hallu;
        }
        else
        {
            BurstType = GasTypes.Steam;
        }
        if (IsAlwaysBurst > 99.9)
        {
            var m = Burst.main;
            m.loop = true;
            Burst.Play();
            AlwaysActive = true;
            LoopHiss.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!AlwaysActive)
        {
            var Rand = Random.Range(0, 500000);
            if (Rand == 3)
            {
                Hiss.Play();
                Invoke("BurstPipe", 4);

            }
        }
       
    }
    void BurstPipe()
    {
        Burst.Play();
        
    }
    private void OnParticleCollision(GameObject other)
    {

        if (other.name.Contains("Player"))
        {
            switch (BurstType)
            {
                case GasTypes.Steam:
                    other.GetComponent<PlayerStats>().TakeDamage(0.4f, "Died of Steam Burns");
                    break;
                case GasTypes.Hallu:
                    other.GetComponent<PlayerStats>().Sanity -= 0.3f;
                    break;
                case GasTypes.Tranq:
                    other.GetComponent<PlayerStats>().VisFatigue += 0.1f;
                    break;

            }
          
        }
            
        
    }
}
