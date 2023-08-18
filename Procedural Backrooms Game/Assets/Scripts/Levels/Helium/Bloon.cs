using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public List<Material> PossibleMats;
    public AudioSource Pop;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = PossibleMats[Random.Range(0, PossibleMats.Count)];
        Pop = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponentInParent<DamageSpear>())
        {
            Pop.Play();
            Destroy(meshRenderer);
            Destroy(gameObject, 5);
        }
      
    }
}
