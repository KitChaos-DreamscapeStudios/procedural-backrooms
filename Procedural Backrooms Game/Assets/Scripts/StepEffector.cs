using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffector : MonoBehaviour
{
    public ParticleSystem Splash;
    public List<AudioClip> StepNoises;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Splash = GetComponent<ParticleSystem>(); 
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position - new Vector3(0, 1,0);
        Player.GetComponent<CurLevelManager>().StepFX = Splash;
    }
}
