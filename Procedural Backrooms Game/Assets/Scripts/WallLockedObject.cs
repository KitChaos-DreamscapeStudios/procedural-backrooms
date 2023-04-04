using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLockedObject : MonoBehaviour
{
    public float Height;
    // Start is called before the first frame update
    void Start()
    {
        var Rotations = new List<int>
        {
            0,
            90,
            -90,
            180
        };
        transform.eulerAngles = new Vector3(0, Rotations[Random.Range(0, Rotations.Count)], 0);
        var hit = new RaycastHit();
        Physics.Raycast(transform.position + transform.right, transform.right, out hit);
        transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
