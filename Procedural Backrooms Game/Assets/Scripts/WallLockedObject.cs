using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLockedObject : MonoBehaviour
{
    public float Height;
    public Vector3 offset;
    float Elap;
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
        transform.position += transform.right.normalized * offset.x;
        transform.position += transform.forward.normalized * offset.z;
    }

    // Update is called once per frame
    void Update()
    {
       
       
       
      
    }
    public void Lock()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
