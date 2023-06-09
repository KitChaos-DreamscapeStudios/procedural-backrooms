using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, GameObject.Find("Player").layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<StepEffector>())
        {
            var FX = col.gameObject.GetComponent<StepEffector>();
            if (FX.Splash)
            {
                FX.Splash.Play();
            }
           
            var NewG = new GameObject();
            NewG.transform.position = transform.position;
           var Src= NewG.AddComponent<AudioSource>();
            Src.clip = FX.StepNoises[Random.Range(0, FX.StepNoises.Count)];
            Src.Play();
            Destroy(Src, 10);
           
        }
        Destroy(gameObject);
    }
}
