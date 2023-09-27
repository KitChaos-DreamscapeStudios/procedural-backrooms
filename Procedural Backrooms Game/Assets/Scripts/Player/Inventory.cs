using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;
    public Item HandItem;
    public GameObject InventScreen;
    public RaycastHit Grabby;
    public float GrabDistance = 10;
    public ParticleSystem SelFX;
    public Image handItemSprite;
    public AudioSource PickupNoise;
    public TMPro.TextMeshProUGUI PickUpTooltip;
    public Dictionary<string, bool> HasReadScripts;
    
    // Start is called before the first frame update
    void Start()
    {
        HasReadScripts = new Dictionary<string, bool>();
    }

    // Update is called once per frame
    void Update()
    {
       
            
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(HandItem != null)
            {
                
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            InventScreen.SetActive(!InventScreen.activeSelf);
        }
        if (Input.GetMouseButtonUp(1) && HandItem!= null)
        {
            HandItem.Use();
        }
        if(HandItem != null)
        {
            HandItem.transform.position = Camera.main.transform.TransformPoint(Vector3.forward + HandItem.HeldPos);
            
        }
        else
        {
           
        }
       var Pickup= Physics.Raycast(GetComponent<Movement3D>().cam.transform.transform.position + GetComponent<Movement3D>().cam.transform.forward, GetComponent<Movement3D>().cam.transform.forward, out Grabby, maxDistance: GrabDistance);
        if (Pickup)
        {
            if (Grabby.collider.transform.parent)
            {
                if (Grabby.collider.transform.parent.GetComponent<PickupAble>())
                {
                    PickUpTooltip.text = $"Pick up { Grabby.collider.transform.parent.GetComponent<PickupAble>().ToGive.ItemName}";

                    SelFX.transform.position = Grabby.point;
                    SelFX.Play();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (Items.Count < 12)
                        {
                            PickupNoise.Play();
                            SelFX.Stop();
                            Grabby.collider.transform.parent.GetComponent<PickupAble>().ToGive.playerStats = GetComponent<PlayerStats>();
                            var NewInvItem = Instantiate(Grabby.collider.transform.parent.GetComponent<PickupAble>().ToGive.gameObject);
                            NewInvItem.transform.position = transform.position + new Vector3(0, 100, 0);//will
                            DontDestroyOnLoad(NewInvItem);
                            Items.Add(NewInvItem.GetComponent<Item>());
                            
                            Destroy(Grabby.collider.transform.parent.gameObject);
                           
                        }
                    }
                }
                else if(!Grabby.collider.gameObject.GetComponent<Interactable>())
                {
                    SelFX.Stop();
                    if (GetComponent<PlayerStats>().SleepButtonTimer > 2)
                    {
                        PickUpTooltip.text = "";
                    }
                    
                }
                else
                {
                    PickUpTooltip.text = Grabby.collider.gameObject.GetComponent<Interactable>().InteractText;
                    Debug.Log("Touching Interactable");
                    SelFX.transform.position = Grabby.point;
                    SelFX.Play();
                    if (Input.GetKeyDown(KeyCode.E) && !Grabby.collider.gameObject.GetComponent<Interactable>().DoNotAllowInteraction)
                    {
                        Grabby.collider.gameObject.GetComponent<Interactable>().OnInteract();
                    }
                }
            }
            else
            {
                SelFX.Stop();
                PickUpTooltip.text = "";
            }

        }
        else
        {
            SelFX.Stop();
            PickUpTooltip.text = "";
        }





    }
}
