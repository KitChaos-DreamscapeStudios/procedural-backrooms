using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 Point;
    float SpeedSlow;
    Rigidbody body;
    public bool AllowHeightChange;
    // Start is called before the first frame update
    void Start()
    {
        Point = transform.position;
        body = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0, 0, 0);
        SpeedSlow = Random.Range(16, 25);
        AssignNewPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targ = Point;

        Vector3 objectPos = transform.position;
        targ -= objectPos;
      
        transform.forward = Vector3.Slerp(transform.forward, targ, 0.1f);
        body.velocity = (transform.forward.normalized*40) / SpeedSlow;
        if(Vector3.Distance(transform.position, Point) <= 2f)
        {
            AssignNewPoint();
        }
    }
    void AssignNewPoint()
    {
        var TempPoint = transform.position+ Random.onUnitSphere * 15;
        if (!AllowHeightChange)
        {
            TempPoint.y = transform.position.y;
        }
       
        RaycastHit hit;
       
        if(Physics.Linecast(transform.position,  TempPoint, out hit))
        {
           // Debug.Log(hit.collider.name);
            
            TempPoint = hit.point;
            if (!AllowHeightChange)
            {
                TempPoint.y = transform.position.y;
            }
           
            if (Vector3.Distance(TempPoint, transform.position) <= .8f)
            {
                goto Skip;
            }
        }

        
        Point = TempPoint;
        goto Jump;
        Skip:;
        Point = transform.position;
        Jump:;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Point);
        Gizmos.DrawSphere(Point, 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        AssignNewPoint();
    }
}
