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
    PlayerStats stats;
    float AwaitGatherance;
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
        stats = Player.GetComponent<PlayerStats>();
        //body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.Sanity < 50)
        {
            AwaitGatherance += Time.deltaTime / Random.Range(1, 4);
            if(AwaitGatherance > 20)
            {
                transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.02f);
                if(AwaitGatherance > 23 || Vector3.Distance(transform.position, Player.transform.position) < 2)
                {
                    AwaitGatherance = 0;
                }
            }
        }
        if (PendDestroyBody)
        {
            elapDestroyBody += Time.deltaTime;
        }
        try
        {
            if (Vector3.Distance(transform.position, Player.transform.position) >= 50)
            {
                meshRenderer.enabled = false;
            }
            else
            {
                meshRenderer.enabled = true;
            }
        }
        catch
        {

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
            //Pop.Play();
            
            Destroy(gameObject);
        }
        if (!col.gameObject.GetComponent<Bloon>())
        {
            body.isKinematic = false;

        }
       
      
    }
    
}
