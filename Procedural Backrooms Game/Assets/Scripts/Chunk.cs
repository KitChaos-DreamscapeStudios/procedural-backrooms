using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public GameObject Layout;
    public List<GameObject> SpawnedDisposables;
    public float SanityDrain;

   // public GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerObj = GameObject.Find("Player");
    }
    public void SpawnStuff(GameObject Struct)
    {
        Rotations.Add(0);
        Rotations.Add(90);
        Rotations.Add(-90);
        Rotations.Add(180);
        //Place the Layout
        Layout = Instantiate(Struct, transform.position, Quaternion.identity);
        //Randomize The Layout's Rotation
        Layout.transform.eulerAngles = new Vector3(0, Rotations[Random.Range(0, Rotations.Count)], 0);
        for (int i = 0; i < 10; i++)
        {
            var NewObj = WallLockedObjects[Random.Range(0, WallLockedObjects.Count)];
            var NewNewObj = Instantiate(NewObj, transform.position + new Vector3(Random.Range(-50, 50), NewObj.GetComponent<WallLockedObject>().Height, Random.Range(-50, 50)), Quaternion.identity);
            //NewNewObj.GetComponent<WallLockedObject>().Lock();
            SpawnedDisposables.Add(NewNewObj);
        }
    }
    private void OnDestroy()
    {
        foreach (var item in SpawnedDisposables)
        {
            Destroy(item);
        }
        Destroy(Layout);
        Layout = null;
    }

    private void OnDrawGizmosSelected()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        SpawnedDisposables = SpawnedDisposables.Where(item => item != null).ToList();
        PlayerIn = Physics.CheckBox(transform.position, new Vector3(40, 200, 40), new Quaternion(0, 0, 0, 0), Player);
       
        if (PlayerIn)
        {
            Parent.PlayerIn = this;
            Parent.SanityDrain = SanityDrain;
        }
        
       
    }
}
