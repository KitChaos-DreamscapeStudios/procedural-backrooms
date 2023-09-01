using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFinder : Item
{
    
    public GameObject Pin;
    public SpriteRenderer Screen;
    
    public Inventory inventory;
    public Battery AttatchedBattery;
    public bool Active;
    TMPro.TextMeshProUGUI Alert;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        //Light = playerStats.Flashlight;
        inventory = playerStats.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

        if (inventory.HandItem == null || !inventory.HandItem)
        {
            
            Active = false;

        }
        if (Active)
        {
            if (AttatchedBattery == null || AttatchedBattery.RemainingCharge <= 0)
            {
                Active = false;
                
            }
        }


        if (Active)
        {
            AttatchedBattery.RemainingCharge -= 0.3f * Time.deltaTime;
            GameObject NearestExit = null;
            try
            {
                NearestExit = gameObject.GetClosestEnemy(GameObject.FindGameObjectsWithTag("Exit"));
            }
            catch
            {

            }
            if (NearestExit)
            {
                Screen.enabled = false;
                Vector3 ModifiedExitPos = NearestExit.transform.position;
                ModifiedExitPos.y = Pin.transform.position.y;
                Pin.transform.eulerAngles = new Vector3(-28.817f, transform.eulerAngles.y, transform.eulerAngles.z);
                Pin.transform.LookAt(ModifiedExitPos);
            }
            else
            {
                Screen.enabled = true;
                Screen.color = new Color(1, 1, 1);
            }
        }
        else
        {
            Screen.enabled = true;
            Screen.color = new Color(0,0, 0);

        }
    }
    public override void Use()
    {
        // throw new System.NotImplementedException();









        Debug.Log("Use");
        if (AttatchedBattery != null)
        {
            if (AttatchedBattery.RemainingCharge > 0)
            {
               
                Active = !Active;
            }
        }
        else
        {
            var DidGetBat = FindBattery();
            if (DidGetBat)
            {
                Use();
            }

        }






    }
    bool FindBattery()
    {
        foreach (Item item in inventory.Items)
        {
            if (item.GetComponent<Battery>())
            {
                var Bat = item.GetComponent<Battery>();
                if (!Bat.InUse)
                {
                    Bat.InUse = true;
                    AttatchedBattery = Bat;
                    Bat.AttatchedItem = this;
                    return true;
                }
            }
        }
        return false;
    }
}
