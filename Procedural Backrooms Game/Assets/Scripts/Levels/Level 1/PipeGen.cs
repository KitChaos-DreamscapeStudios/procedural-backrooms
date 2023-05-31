using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGen : MonoBehaviour
{
    public GameObject lastPipe;
    public Vector3 Curpoint;
    public GameObject NormalPipe;
    public GameObject ValvePipe;
    public GameObject CurvePipe;
    bool StopGen;
    // Start is called before the first frame update
    void Start()
    {
        Curpoint = transform.position;
        lastPipe = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StopGen)
        {
            MakeNewPipe();
        }
        
    }
    void MakeNewPipe()
    {
        var Rand = Random.Range(0, 20);
        GameObject Fab = NormalPipe;
        if(Rand < 5)
        {
            Fab = CurvePipe;
        }
        else if(Rand == 6)
        {
            Fab = ValvePipe;
        }
        else
        {
            Fab = NormalPipe;
        }

        var NewPipe = Instantiate(Fab, Curpoint + (lastPipe.transform.forward + lastPipe.transform.eulerAngles) * 1.98f, Quaternion.identity);
        if(Fab != CurvePipe)
        {
            NewPipe.transform.eulerAngles = lastPipe.transform.eulerAngles;
        }
        else
        {
            var TurnRand = Random.Range(0, maxInclusive: 10);
            if(TurnRand < 5)
            {
                NewPipe.transform.eulerAngles = lastPipe.transform.forward + new Vector3(0, 0, 0);
                var NextPipe = Instantiate(NormalPipe, NewPipe.transform.position + NewPipe.transform.right * 1.98f, Quaternion.identity);
                NextPipe.transform.eulerAngles = NewPipe.transform.eulerAngles + new Vector3(0, 90, 0);

                lastPipe = NextPipe;
            }
            else if(TurnRand == 6)
            {
                NewPipe.transform.eulerAngles = lastPipe.transform.forward + new Vector3(0, 0, 90);
                StopGen = true;
               // NewPipe.transform.position += new Vector3(1.36913455f, 0.0120877391f, 6.95486689f) - new Vector3(0.0199999996f, 1.33000004f, 6.95486689f); ;
               
            }
            else
            {
                NewPipe.transform.eulerAngles = lastPipe.transform.forward + new Vector3(0, 90, 0);
                //NewPipe.transform.position += new Vector3(1.36913455f, 0.0120877391f, 6.95486689f) - new Vector3(-1.7334522f, 0.0120877391f, 6.40634775f);
                var NextPipe = Instantiate(NormalPipe, NewPipe.transform.position -NewPipe.transform.right *1.98f, Quaternion.identity);
                NextPipe.transform.eulerAngles = NewPipe.transform.eulerAngles + new Vector3(0,90,0);
                lastPipe = NextPipe;



            }
        }
        
        Curpoint = NewPipe.transform.position;

    }
}
