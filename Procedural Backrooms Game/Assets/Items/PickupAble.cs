using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour
{
    public Item ToGive;
    public bool DontRemoveParent;
    // Start is called before the first frame update
    void Start()
    {
        if (!DontRemoveParent)
        {
            transform.parent = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
