using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : Interactable
{
  
    bool IsChanging;
    public string SceneToLoad;
    public List<AudioClip> Steps;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public override void OnInteract()
    {
        var Player = GameObject.Find("Player");
        Player.transform.position = new Vector3(0, 3.47f, 0);
        Player.GetComponent<CurLevelManager>().CurStepNoises = Steps;
        SceneManager.LoadScene(SceneToLoad);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
   
}

