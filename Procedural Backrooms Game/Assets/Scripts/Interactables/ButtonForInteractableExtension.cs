using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForInteractableExtension : Interactable
{
    public Interactable Targ;
    public override void OnInteract()
    {
        Targ.OnInteract();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
