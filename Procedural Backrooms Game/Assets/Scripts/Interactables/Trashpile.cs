using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashpile : Interactable
{
    public List<GameObject> Droppables;
    public List<GameObject> PossibleDroppables;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Random.Range(3, 8); i++)
        {
            Refresh();
        }
    }
    public override void OnInteract()
    {
        var TargItem = Droppables[Random.Range(0, Droppables.Count)];
        var DroppedItem = Instantiate(TargItem, transform.position, Quaternion.identity);
        if (DroppedItem.GetComponent<Rigidbody>())
        {
            DroppedItem.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-3, 4), Random.Range(0, 4)));

        }

        Droppables.Remove(TargItem);


    }

    // Update is called once per frame
    void Update()
    {
        if(Droppables.Count <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void Refresh()
    {
        Droppables.Add(PossibleDroppables[Random.Range(0, PossibleDroppables.Count)]);
    }
}
