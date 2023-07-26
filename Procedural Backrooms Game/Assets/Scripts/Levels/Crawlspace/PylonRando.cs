using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonRando : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), Random.Range(-45, 45));
        transform.position += new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
