using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public string LockName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract()
    {
        if (GameObject.Find("Player").GetComponent<Inventory>().HandItem.GetComponent<Key>())
        {
            if(GameObject.Find("Player").GetComponent<Inventory>().HandItem.GetComponent<Key>().KeyName == LockName)
            {
                Destroy(gameObject);
            }
        }
    }
}
