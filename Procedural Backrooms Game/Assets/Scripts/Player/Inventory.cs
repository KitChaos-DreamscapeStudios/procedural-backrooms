using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;
    public Item HandItem;
    public GameObject InventScreen;
    public RaycastHit Grabby;
    public float GrabDistance = 10;
    public ParticleSystem SelFX;
    public MeshFilter handItemMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            InventScreen.SetActive(!InventScreen.activeSelf);
        }
        if (Input.GetMouseButtonUp(1) && HandItem!= null&& HandItem.isHoldable)
        {
            HandItem.UseHeld();
        }
        if(HandItem != null)
        {
            handItemMesh.mesh = HandItem.Model;
        }
        else
        {
            handItemMesh.mesh = null;
        }
       var Pickup= Physics.Raycast(GetComponent<Movement3D>().cam.transform.transform.position + GetComponent<Movement3D>().cam.transform.forward, GetComponent<Movement3D>().cam.transform.forward, out Grabby, maxDistance: GrabDistance);
        if (Pickup)
        {
            if (Grabby.collider.transform.parent)
            {
                if (Grabby.collider.transform.parent.GetComponent<PickupAble>())
                {
                    SelFX.transform.position = Grabby.point;
                    SelFX.Play();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (Items.Count < 12)
                        {
                            SelFX.Stop();
                            Items.Add(Grabby.collider.transform.parent.GetComponent<PickupAble>().ToGive);
                            Destroy(Grabby.collider.transform.parent.gameObject);
                        }
                    }
                }
                else if(!Grabby.collider.gameObject.GetComponent<Interactable>())
                {
                    SelFX.Stop();
                }
                else
                {
                    Debug.Log("Touching Interactable");
                    SelFX.transform.position = Grabby.point;
                    SelFX.Play();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Grabby.collider.gameObject.GetComponent<Interactable>().OnInteract();
                    }
                }
            }
            else
            {
                SelFX.Stop();
            }

        }
        else
        {
            SelFX.Stop();
        }





    }
}
