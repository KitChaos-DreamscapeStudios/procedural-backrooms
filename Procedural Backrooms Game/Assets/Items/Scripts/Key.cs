using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public string KeyName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Use()
    {
        Debug.Log(KeyName);
    }
}
