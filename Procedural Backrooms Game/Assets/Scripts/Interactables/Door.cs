using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public GameObject Pivot;
    bool IsChanging;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public override void OnInteract()
    {
        IsChanging = true;
      
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsChanging)
        {
           Pivot.transform.eulerAngles=  Vector3.Lerp(Pivot.transform.eulerAngles, new Vector3(0, -90, 0), 0.1f);
            if(Pivot.transform.eulerAngles.y == -90)
            {
                IsChanging = false;
            }
        }
    }
   
}

