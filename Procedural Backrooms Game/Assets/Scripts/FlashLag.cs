using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLag : MonoBehaviour
{

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.Slerp(transform.forward, Camera.main.transform.forward, 0.2f);
        transform.position = Camera.main.transform.position;
    }
}
