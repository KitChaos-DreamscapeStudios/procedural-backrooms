using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{
    public List<GameObject> Items;

    // Start is called before the first frame update
     void Start()
    {
        List<GameObject> removers = new List<GameObject>();
        foreach(GameObject i in Items)
        {
            if(Random.Range(0, 100) > 65)
            {
                removers.Add(i);
            }
        }
        foreach(GameObject i in removers)
        {
            Items.Remove(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract()
    {
       foreach(GameObject i in Items)
        {
            Instantiate(i, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
