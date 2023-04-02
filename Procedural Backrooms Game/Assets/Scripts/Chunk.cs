using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    
    public bool PlayerIn;
    public Generation Parent;
    public LayerMask Player;
    public Coords coords;
    public List<GameObject> Structs;
    public List<float> Rotations;
    public List<GameObject> FreeStandingObjects;//Some Furniture, Entities. (Lock to floor)
    public List<GameObject> WallLockedObjects;//Doors, Paintings, Vents. (Lock to walls)
    // Start is called before the first frame update
    void Start()
    {
        Rotations.Add(0);
        Rotations.Add(90);
        Rotations.Add(-90);
        Rotations.Add(180);
    }
    public void SpawnStuff(GameObject Struct)
    {
        //Place the Layout
        var Layout = Instantiate(Struct, transform.position, Quaternion.identity);
        //Randomize The Layout's Rotation
        Layout.transform.eulerAngles = new Vector3(0, Rotations[Random.Range(0, Rotations.Count)], 0);
    }
    

    // Update is called once per frame
    void Update()
    {
        PlayerIn = Physics.CheckBox(transform.position, new Vector3(transform.lossyScale.x, 10, transform.lossyScale.y), new Quaternion(0, 0, 0, 0), Player);
        if (PlayerIn)
        {
            Parent.PlayerIn = this;
        }
    }
}
