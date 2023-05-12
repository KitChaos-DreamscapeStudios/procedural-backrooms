using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : Interactable
{
    public GameObject Pivot;
    bool isOpening;
    // Start is called before the first frame update
    void Start()
    {
        Pivot = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            Pivot.transform.eulerAngles = Vector3.Lerp(Pivot.transform.eulerAngles, new Vector3(0, 90, 0), 0.05f);
        }   
    }
    public override void OnInteract()
    {
        isOpening = true;
    }
}
