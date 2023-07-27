using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : Interactable
{
    public GameObject Pivot;
    public bool AlreadyAligned;
    bool IsOpen;
    // Start is called before the first frame update
    void Start()
    {
        
            Pivot = transform.parent.gameObject;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpen)
        {
            InteractText = "Close Door";
            if (!AlreadyAligned)
            {
                Pivot.transform.localEulerAngles = Vector3.Lerp(Pivot.transform.localEulerAngles, new Vector3(0, 90, 0), 0.05f);
            }
            else
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 90, 0), 0.05f);
            }
           
        }
        else
        {
            InteractText = "Open Door";
            if (AlreadyAligned)
            {
                Pivot.transform.localEulerAngles = Vector3.Lerp(Pivot.transform.localEulerAngles, new Vector3(0, 0, 0), 0.05f);
            }
            else
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, 0), 0.05f);
            }
          
        }
    }
    public override void OnInteract()
    {
        IsOpen = !IsOpen;
    }
}
