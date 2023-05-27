using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecHolder : MonoBehaviour
{
    public string Specialization;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
