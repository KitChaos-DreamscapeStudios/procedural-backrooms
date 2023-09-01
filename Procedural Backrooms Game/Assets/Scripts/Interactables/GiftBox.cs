using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A Giftbox is Like a crate but it only and always drops one item
/// They can also trigger partygoers
/// </summary>
public class GiftBox : Interactable
{
    
    public bool DoTriggerPartyGoers;
    public List<GameObject> PossibleDrops;
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
        Instantiate(PossibleDrops[Random.Range(0, PossibleDrops.Count)], transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (DoTriggerPartyGoers)
        {
         
            
                var Attacker = gameObject.GetClosestEnemy(GameObject.FindGameObjectsWithTag("Party"));
            if (Attacker)
            {
                if (Vector3.Distance(transform.position, Attacker.transform.position) < 200)
                {
                    Attacker.GetComponent<Partygoer>().State = Partygoer.PartyState.Warping;
                }
            }
            
           
            
           
        }
    }
   

}


