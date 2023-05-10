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
                Invoke("FootStep", RegulateAbove0(1.5f - ((int)movement.speed / 6)));
            }
            else
            {
                Invoke("FootStep", RegulateAbove0(2.5f - ((int)movement.speed / 8)));
            }
           
            

        }
       
        
    }
    float RegulateAbove0(float Input)//used to make sure footstepts dont jank out
    {
        if(Input <= 0)
        {
            Input = 0.5f;
        }
        return Input;
    }
    void FootStep()
    {
        if (movement.FB != 0)
        {

           
                var fs = Instantiate(footStep.gameObject);
                fs.GetComponent<AudioSource>().clip = CurStepNoises[Random.Range(0, CurStepNoises.Count)];
            fs.GetComponent<AudioSource>().pitch = Random.Range(-0.2f, 1.2f);
                fs.GetComponent<AudioSource>().Play();

                Destroy(fs, 3);
                footTimer = 0;
            
        }
        
    }
  
    
}
