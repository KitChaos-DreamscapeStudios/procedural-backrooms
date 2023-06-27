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
    // Start is called before the first frame update
    void Start()
    {
        ItemTravelpoint = transform.position + ItemTravelpoint;
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
        }
        if (IsInUse)
        {
            GameObject.Find("Player").GetComponent<Movement3D>().SoundLevel = 90;
            TargetItem.transform.position = Vector3.Lerp(TargetItem.transform.position, ItemTravelpoint, 0.08f);
            if(Vector3.Distance(TargetItem.transform.position, ItemTravelpoint) < 0.5f)
            {
              
                if (!IsStuck)
                {
                    var Rand = Random.Range(0, 100);
                    if(Rand >= 65)
                    {
                        IsStuck = true;
                        IsInUse = false;
                        Vend.Stop();
                    }
                    else
                    {
                        ItemTravelpoint = ItemDropPoint;
                        EnableColliders(TargetItem, true);
                    }
                }
            }
        }
    }
    public override void OnInteract()
    {
        if(!IsInUse && !IsStuck && Items.Count > 1)
        {
            IsInUse = true;
            TargetItem = Items[0];

        }
        if (IsStuck)
        {
            Vector3 CurPos = transform.position;
            for (int i = 0; i < 5; i++)
            {
                transform.position += new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
                transform.position = CurPos;
            }
            transform.position = CurPos;
           
            Shake.Play();
            var Rand = Random.Range(0, 100);
            GameObject.Find("Player").GetComponent<Movement3D>().SoundLevel = 200;
            if(Rand > 85)
            {
                IsInUse = true;
                IsStuck = false;
                ItemTravelpoint = ItemDropPoint;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(ItemTravelpoint + transform.position, 0.1f);
        Gizmos.DrawSphere(ItemDropPoint + transform.position, 0.1f);
    }
}
