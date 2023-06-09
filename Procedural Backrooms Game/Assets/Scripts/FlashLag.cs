using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLag : MonoBehaviour
{
    Quaternion LastPos;
    public GameObject Orient;
    // Start is called before the first frame update
    void Start()
    {
      //  DontDestroyOnLoad(gameObject);
        LastPos = transform.parent.rotation;
        InvokeRepeating("UpdatePos", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
         transform.rotation = Quaternion.Slerp(transform.rotation, LastPos, 0.1f);
    }
    void UpdatePos()
    {
        LastPos = transform.parent.rotation;
    }
}
