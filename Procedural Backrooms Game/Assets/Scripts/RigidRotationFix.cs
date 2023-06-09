using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidRotationFix : MonoBehaviour
{
    public Vector3 DefScale = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Unparent", 0.5f); 
    }
    void Unparent()
    {
        transform.parent = null;
        transform.localScale = DefScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
