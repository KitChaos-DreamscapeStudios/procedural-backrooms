using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : Interactable
{
    public GameObject Pivot;
    bool IsChanging;
    public string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public override void OnInteract()
    {

        SceneManager.LoadScene(SceneToLoad);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
   
}

