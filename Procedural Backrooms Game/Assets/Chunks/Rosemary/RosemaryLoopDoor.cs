using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosemaryLoopDoor : Interactable
{
    public GameObject Player;
    public GameObject Layout;
    public GameObject CurLayout;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        CurLayout = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract()
    {
        Player.transform.position = new Vector3(0, 3.47f, 0);
        Instantiate(Layout, new Vector3(4.98999977f, 0, -6.26999998f), Quaternion.identity);
        Destroy(CurLayout);
    }
}
