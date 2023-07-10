using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class VendingMachine : Interactable
{
    public bool IsInUse;
    public bool IsStuck;
    
    public List<GameObject> Items;
    public Vector3 ItemTravelpoint;
   [SerializeField] GameObject TargetItem;
    public Vector3 ItemDropPoint;
    public AudioSource Shake;
    public AudioSource Vend;
    Vector3 BaseTravelPoint;
    // Start is called before the first frame update
    void Start()
    {
        ItemTravelpoint = transform.position + ItemTravelpoint;
        ItemTravelpoint = BaseTravelPoint;
        ItemDropPoint = transform.position + ItemDropPoint;
        
        foreach(GameObject i in Items)
        {
            EnableColliders(i, false); 
        }
    }
    public void EnableColliders(GameObject Which, bool state)
    {
        var Cols = Which.GetComponentsInChildren<Collider>();
       

        foreach (Collider c in Cols)
        {
            c.enabled = state;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsStuck)
        {
            InteractText = "Shake Vending Machine";
        }
        else
        {
            InteractText = "Vend";

        }
        if(TargetItem == null)
        {
            Items = Items.Where(item => item != null).ToList();
            IsInUse = false;
            TargetItem = Items[0];
            ItemTravelpoint = BaseTravelPoint;
        }
        if (IsInUse)
        {
            GameObject.Find("Player").GetComponent<Movement3D>().SoundLevel = 90;
            TargetItem.transform.position = Vector3.Lerp(TargetItem.transform.position, ItemTravelpoint, 0.08f);
            if(Vector3.Distance(TargetItem.transform.position, ItemTravelpoint) < 0.1f)
            {
              
                if (ItemTravelpoint != ItemDropPoint)
                {
                   
                   
                    {
                        ItemTravelpoint = ItemDropPoint;
                        EnableColliders(TargetItem, true);
                    }
                }
                else
                {
                    IsInUse = false;
                }
            }
        }
        else
        {
            Vend.Stop();
        }
    }
    public override void OnInteract()
    {
        if(!IsInUse && !IsStuck && Items.Count > 1)
        {
            IsInUse = true;
            TargetItem = Items[0];
            Vend.Play();

        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(ItemTravelpoint + transform.position, 0.1f);
        Gizmos.DrawSphere(ItemDropPoint + transform.position, 0.1f);
    }
}
