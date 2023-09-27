using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPipe : MonoBehaviour
{
    public float AwaitDepositGarbage;
    public Trashpile ConnectedPile;
    float TimeTillTrash;
    public GameObject Trashball;
    public Vector3 TrashBallDrop;
    // Start is called before the first frame update
    void Start()
    {
        TimeTillTrash = Random.Range(10, 40);
        TrashBallDrop = transform.position + TrashBallDrop;
    }

    // Update is called once per frame
    void Update()
    {
        AwaitDepositGarbage += Time.deltaTime;
        if (AwaitDepositGarbage > TimeTillTrash)
        {
            TimeTillTrash = Random.Range(10, 40);
            AwaitDepositGarbage = 0;
            var Garball =Instantiate(Trashball, TrashBallDrop, Quaternion.identity);
            Destroy(Garball, 2);
            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                ConnectedPile.Refresh();
            }
           
        }
    }
}
