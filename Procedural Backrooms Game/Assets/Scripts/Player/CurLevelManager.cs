using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class CurLevelManager : MonoBehaviour
{
    public Movement3D movement;
    public AudioSource footStep;
    public List<AudioClip> CarpetWalkNoises;
    public string WalkArea;
    public List<AudioClip> CurStepNoises;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Step");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Step()
    {
        if (movement.FB != 0)
        {
            footStep.clip = CurStepNoises[Random.Range(0, CurStepNoises.Count)];
            footStep.Play();
            yield return new WaitForSecondsRealtime((2.5f - (int)movement.speed)/10);
            Step();

        }
        else
        {
            Step();
        }
    }
    
}
