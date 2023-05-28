using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWallUp : MonoBehaviour
{
    float Elap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 5.51070023f, transform.position.y),0.1f);
        Elap += Time.deltaTime;
        if(Elap > 30)
        {
            Destroy(gameObject);
        }
    }
}
