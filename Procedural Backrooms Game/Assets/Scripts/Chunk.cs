using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using Unity.AI.Navigation;
using System.Threading.Tasks;

public class Chunk : MonoBehaviour
{
    public NavMeshSurface NavSurf;
    
  
    public bool PlayerIn;
    public Generation Parent;
    public LayerMask Player;
    public Coords coords;
    public List<GameObject> Structs;
    
    public List<GameObject> FreeStandingObjects;//Some Furniture, Entities. (Lock to floor)
    public List<GameObject> WallLockedObjects;//Doors, Paintings, Vents. (Lock to walls)
    public GameObject Layout;
    public List<GameObject> SpawnedDisposables;
    public float SanityDrain;
    public List<Vector3> ItemSpawnLocales;
    public delegate void Func();
   // public GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerObj = GameObject.Find("Player");
    }

    public async Task BuildNav()
    {
        NavSurf = GetComponentInChildren<NavMeshSurface>();
        Task.Run(NavSurf.BuildNavMesh);
        //BuildNav();

    }
    public unsafe void SpawnStuff(GameObject Struct)
    {
        const int rotLen = 4;
       int* rots = stackalloc int[rotLen]; 
        rots[0] = 0;
        rots[1] = 90;
        rots[2] = -90;
        rots[3] = 180;
        
        //Place the Layout
        Layout = Instantiate(Struct, transform.position, Quaternion.identity);
        //Randomize The Layout's Rotation
        Layout.transform.eulerAngles = new Vector3(0, rots[Random.Range(0, 4)], 0);
        // Invoke("BuildMesh", 2);

        BuildNav();

       



    }
    public void BuildMesh()
    {
       
    }
    private void OnDestroy()
    {
        CancelInvoke();
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
