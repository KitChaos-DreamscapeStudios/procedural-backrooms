using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public float Volume;
    public Slider VolSlider;
    bool IsPaused;
    public GameObject SettingsMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Application.targetFrameRate = 60;
        //Screen.SetResolution(1920, 1080, true);
        Volume = VolSlider.value;
        foreach(AudioSource a in GameObject.FindObjectsOfType<AudioSource>())
        {
            if(a.tag != "OwnVolume")
            {
                a.volume = Volume;
            }
           
        }
        if (IsPaused)
        {
          //  Time.timeScale = 0;
            SettingsMenu.SetActive(true);
            GetComponent<Inventory>().InventScreen.SetActive(false);
            GetComponent<Movement3D>().CantMove = true;
        }
        else
        {
            //Time.timeScale = 1;
            SettingsMenu.SetActive(false);
            GetComponent<Movement3D>().CantMove = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }
    
}
