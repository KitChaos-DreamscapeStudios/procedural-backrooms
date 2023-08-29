using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public List<Material> PossibleMats;
    public AudioSource Pop;
    public GameObject Player;
    bool PendDestroyBody;
    public Rigidbody body;
    float elapDestroyBody;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
       
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = PossibleMats[Random.Range(0, PossibleMats.Count)];
        Pop = GetComponent<AudioSource>();
        Vector3 EditPos = Random.insideUnitSphere * 40;
        EditPos.y =Random.Range(-2, 3);
        transform.position += EditPos;
        Player = GameObject.Find("Player");
        //body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PendDestroyBody)
        {
            elapDestroyBody += Time.deltaTime;
        }
        if(Vector3.Distance(transform.position, Player.transform.position) >= 50)
        {
           meshRenderer.enabled = false;
        }
        else
        {
           meshRenderer.enabled = true;
        }
        if (!body.isKinematic)
        {
            PendDestroyBody = true;
            elapDestroyBody = 0;
        }
        else
        {
            PendDestroyBody = false;
        }
        if (elapDestroyBody > 5)
        {
            body.isKinematic = true;
            elapDestroyBody = 0;
            
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        body = GetComponent<Rigidbody>();
        if (col.gameObject.GetComponentInParent<DamageSpear>())
        {
            Pop.Play();
            Destroy(meshRenderer);
            Destroy(gameObject, 5);
        }
        if (!col.gameObject.GetComponent<Bloon>())
        {
            body.isKinematic = false;

        }
       
      
    }
    
}
