using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBurst : MonoBehaviour
{
    public ParticleSystem Burst;
    public AudioSource Hiss;
    // Start is called before the first frame update
    void Start()
    {
        Burst = GetComponent<ParticleSystem>();
        transform.eulerAngles = new Vector3(0, 0, Random.Range(-50,50));
    }

    // Update is called once per frame
    void Update()
    {
        var Rand = Random.Range(0, 500000);
        if(Rand == 3)
        {
            Hiss.Play();
            Invoke("BurstPipe", 4);
        }
    }
    void BurstPipe()
    {
        Burst.Play();
        
    }
    private void OnParticleCollision(GameObject other)
    {
       
       
            GameObject.Find("Player").GetComponent<PlayerStats>().TakeDamage(0.4f, "Died of Steam Burns");
        
    }
}
