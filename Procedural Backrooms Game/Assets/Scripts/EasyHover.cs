using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyHover : MonoBehaviour
{
    public float Timer;
    Vector3 Base;
    // Start is called before the first frame update
    void Start()
    {
        Base = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        transform.position = Base + new Vector3(0,Mathf.Sin(Timer)*20,0);
    }
}
