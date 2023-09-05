using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    PlayerStats Player;
    [SerializeField] GameObject ConnectedObject;
    // Start is called before the first frame update
    void Start()
    {
       
        Player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < 40)
        {
            Player.Sanity += 0.1f * Time.deltaTime;
        }
        if (ConnectedObject)
        {
            Destroy(gameObject);
            GetComponent<AudioSource>().Stop();
            Debug.Log("ShouldNotExist");
        }
    }
}
