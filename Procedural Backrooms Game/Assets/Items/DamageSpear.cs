using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpear : MonoBehaviour
{
    public float Damage;
  
    // Start is called before the first frame update
    void Start()
    {
    //    bod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision col)
    {
        //var Hit = new RaycastHit();
        //var joint = gameObject.AddComponent<FixedJoint>();
        //Physics.Raycast(transform.position, transform.forward, out Hit);
        //joint.anchor = Hit.point;
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //GetComponent<Rigidbody>().ResetInertiaTensor();
    }
}
