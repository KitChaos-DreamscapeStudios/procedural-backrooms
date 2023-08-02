using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpable : MonoBehaviour
{
    public WarehouseGen Parent;
    bool PendDestroyBody;
    // Start is called before the first frame update
    void Start()
    {
        Parent = GameObject.Find("Generation").GetComponent<WarehouseGen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Rigidbody>())
        {
            PendDestroyBody = false;
        }
    }
    void RemBody()
    {
       
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Movement3D>())
        {
            col.gameObject.GetComponent<Movement3D>().SoundLevel = 126;
        }
        Rigidbody body = null;
        if (!GetComponent<Rigidbody>())
        {
             body = gameObject.AddComponent<Rigidbody>();
        }
        else
        {
             body = GetComponent<Rigidbody>();
        }
        if (!PendDestroyBody)
        {
            PendDestroyBody = true;
            Destroy(gameObject.GetComponent<Rigidbody>(), 20);
        }
        body.mass = 10;
        Parent.ThreatLevel += 0.5f;
        var Bonk =Instantiate(Parent.BoxKnock, transform.position, Quaternion.identity);
        Bonk.Play();
        Destroy(Bonk.gameObject, 4);
        
    }
}
