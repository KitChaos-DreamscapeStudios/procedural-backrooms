using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticeDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Working");
        //if (other.layer == 3)
        //{
        //    GameObject.Find("Player").GetComponent<PlayerStats>().TakeDamage(40, "Steam Burns");
        //}
    }
}
