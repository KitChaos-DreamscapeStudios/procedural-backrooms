using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLockedObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var hit = new RaycastHit();
        Physics.Raycast(transform.position - transform.forward, -transform.forward, out hit);
        transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
