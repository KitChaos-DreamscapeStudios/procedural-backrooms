using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
public class BridgePoint : MonoBehaviour
{
    public NavMeshLink Link;
    public Vector3 Direction;
    public LayerMask Walls;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Check", 2);

    }
    void Check()
    {
        Link = GetComponent<NavMeshLink>();
        Link.startPoint = new Vector3(0,0,0);
        Link.endPoint = new Vector3(0,0,0);
        Link.width = 3;
        if (!Physics.Raycast(transform.position, direction: Direction, 3))
        {
            Link.endPoint = new Vector3(3 * Direction.x, 0, 3 * Direction.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
