using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandSpawnObject : MonoBehaviour
{
   
    public float Rarity;
    // Start is called before the first frame update
    void Start()
    {

        
        var rand = Random.Range(0, maxInclusive:100f);
        if(rand < Rarity)
        {
            Destroy(gameObject);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
       
       
       
      
    }
    public void Lock()
    {
       
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
