using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathResonGetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Death Data").GetComponent<DeathReason>().Reason;
    }
}
