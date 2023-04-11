using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLockedObject : MonoBehaviour
{
    public float Height;
    public string ForDirect;
    float Elap;
    // Start is called before the first frame update
    void Start()
    {
       
        Vector3 fordir = transform.right;
        switch (ForDirect)
        {
            case "Forward":
                fordir = transform.forward;
                break;
            case "Right":
                fordir = transform.right;
                break;
            
        }
       // transform.eulerAngles = new Vector3(0, Rotations[Random.Range(0, Rotations.Count)], 0);
        var hit = new RaycastHit();
        Physics.Raycast(transform.position + fordir, fordir, out hit);
        transform.position = hit.point;
      
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
