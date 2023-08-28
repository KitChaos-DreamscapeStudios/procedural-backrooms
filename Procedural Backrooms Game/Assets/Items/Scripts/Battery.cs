using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item
{
    public float RemainingCharge;
    public float MaxCharge;
    public bool Equipped;
    Inventory playerInv;
    bool DoShake;
    Vector3 BasePos;
    public bool InUse;
    public Item AttatchedItem;
    // Start is called before the first frame update
    void Start()
    {
        playerInv = GameObject.Find("Player").GetComponent<Inventory>();
        BasePos = HeldPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(AttatchedItem == null)
        {
            InUse = false;
        }
        ItemName = $"Battery {Mathf.Round((RemainingCharge/MaxCharge)*100)}%";
        if (Equipped)
        {
          if(RemainingCharge <= MaxCharge)
          {
                RemainingCharge += 0.2f *Time.deltaTime;
          }
        }
        if(playerInv.HandItem != this)
        {
            Equipped = false;
        }
        if (DoShake && BasePos != HeldPos)
        {
            HeldPos = Vector3.Lerp(HeldPos, BasePos, 0.1f);
        }
        
    }
    public override void Use()
    {
        Equipped = true;
        // RemainingCharge += 1;
        HeldPos += new Vector3(0, 0.5f, 0);
        DoShake = true;
    }
}
