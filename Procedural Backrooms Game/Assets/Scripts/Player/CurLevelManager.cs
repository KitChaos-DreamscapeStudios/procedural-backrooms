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
    float footTimer;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Step");
        //Lame steps rn, will better
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInvoking("FootStep"))
        {
            if (!movement.sprinting)
            {
                Invoke("FootStep", (1.5f - ((int)movement.speed / 6)));
            }
            else
            {
                Invoke("FootStep", (2.5f - ((int)movement.speed / 8)));
            }
           
            

        }
       
        
    }
    void FootStep()
    {
        if (movement.FB != 0)
        {

           
                var fs = Instantiate(footStep.gameObject);
                fs.GetComponent<AudioSource>().clip = CurStepNoises[Random.Range(0, CurStepNoises.Count)];
                fs.GetComponent<AudioSource>().Play();
                Destroy(fs, 3);
                footTimer = 0;
            
        }
        
    }
  
    
}
