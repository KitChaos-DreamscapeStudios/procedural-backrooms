using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RosemaryHumCounter : MonoBehaviour
{
    public List<LevelData> PossibleWarps;
    public GameObject Player;
    float Timer;
    float QuickTimer;
    LevelData SelectedLevel;
    bool HasStartedMove;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        SelectedLevel = PossibleWarps[Random.Range(0, PossibleWarps.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > 240)
        {
            if (!HasStartedMove)
            {
                Player.GetComponent<PlayerStats>().IsSleeping = true;
            }
            if (!Player.GetComponent<PlayerStats>().IsSleeping)
            {
                SceneManager.LoadScene(SelectedLevel.SceneName);
                Player.GetComponent<CurLevelManager>().CurStepNoises = SelectedLevel.Footsteps;
                Player.transform.position = new Vector3(0, 3.47f, 0);
                Player.GetComponent<PlayerStats>().IsSleeping = false;
            }
            HasStartedMove = true;
           
        }
    }
}
[System.Serializable]
public struct LevelData
{
    public string SceneName;
    public List<AudioClip> Footsteps;
    
}
