using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWindows : MonoBehaviour
{
    //  public static AudioSource Thunder;
    public Material Off;
    MeshRenderer MeshRenderer;
    public Material On;
    public bool IsFlashing;
    float FlashSin;
    public AudioSource Thunder;

    // Learn flares and make an actual flash later;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();    
        Thunder = GameObject.Find("Thunder").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Thunder.isPlaying)
        {
            MeshRenderer.material = On;
            Debug.Log("Playing");
        }
        else
        {
            MeshRenderer.material = Off;
        }
       
    }
    
}
