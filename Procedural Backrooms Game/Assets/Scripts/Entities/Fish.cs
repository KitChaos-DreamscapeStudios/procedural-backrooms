using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 Point;
    float SpeedSlow;
    // Start is called before the first frame update
    void Start()
    {
        SpeedSlow = Random.Range(16, 25);
        AssignNewPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targ = Point;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        targ.z = targ.z - objectPos.z;
        transform.forward = Vector3.Slerp(transform.forward, targ, 0.1f);
        transform.position += transform.forward.normalized / SpeedSlow;
        if(Vector3.Distance(transform.position, Point) <= 3)
        {
            AssignNewPoint();
        }
    }
    void AssignNewPoint()
    {
        var TempPoint = Random.onUnitSphere * 20;
        TempPoint.y = transform.position.y;
        RaycastHit hit;
        if(Physics.Linecast(transform.position, TempPoint, out hit))
        {
            TempPoint = hit.point;
            TempPoint.y = transform.position.y;
        }

        
        Point = TempPoint;

    }
    
}
