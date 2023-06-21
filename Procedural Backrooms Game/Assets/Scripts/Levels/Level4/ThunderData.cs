using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderData : MonoBehaviour
{
   [SerializeField] public static AudioSource Thunder;
    float elap;
    float Rand;
    // Start is called before the first frame update
    void Start()
    {   
        Thunder = GetComponent<AudioSource>();
        Rand = Random.Range(14, 45);
    }

    // Update is called once per frame
    void Update()
    {
        elap += Time.deltaTime;
        if(elap >= Rand)
        {
            Rand = Random.Range(14, 45);
            elap = 0;
            Thunder.Play();

        }
        
    }
}
