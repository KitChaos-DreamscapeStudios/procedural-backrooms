using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonThings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Level0");
    }
    public void BackToMenuButtom()
    {
        SceneManager.LoadScene("Title");
    }
    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }
}
