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
    public Material OffLights;
    
   // public List<GameObject> FreeStandingObjects;//Some Furniture, Entities. (Lock to floor)
   // public List<GameObject> WallLockedObjects;//Doors, Paintings, Vents. (Lock to walls)
    public GameObject Layout;
    public List<GameObject> SpawnedDisposables;
    public float SanityDrain;
    public List<Vector3> ItemSpawnLocales;
    public delegate void Func();
    public List<Light> Lights;
    public float BedQual;
    //level specific stuff //ignore this comment
    //level 0
    public bool IsShiftingLightsB;
    public bool IsShiftingLightsN;
    //level 4
    public List<GameObject> Walls;
    public List<int> rots;//Used for if the chunk is brighting or nighting its lights for level 0
                                // public GameObject PlayerObj;
                                // Start is called before the first frame update
    void Start()
    {
       // PlayerObj = GameObject.Find("Player");
    }

    public async Task BuildNav()
    {
        
        NavSurf = GetComponentInChildren<NavMeshSurface>();
        await Task.Run(NavSurf.BuildNavMesh);
            NavSurf.UpdateNavMesh(NavSurf.navMeshData);
        
        //sTask.Run(NavSurf.AddData);
        //BuildNav();

    }
    public void SpawnStuff(GameObject Struct)
    {
        
        //const int rotLen = 4;
       //int* rots = stackalloc int[rotLen]; 
        rots.Add(0);
        rots.Add(90);
        rots.Add(-90);
        rots.Add(180);
        
        //Place the Layout
        Layout = Instantiate(Struct, transform.position + new Vector3(0, 5.8337f, 0), Quaternion.identity);
        //Randomize The Layout's Rotation
        Layout.transform.eulerAngles = new Vector3(0, rots[Random.Range(0, 4)], 0);
        foreach (Light light in Layout.GetComponentsInChildren<Light>())
        {
            Lights.Add(light);
        }
        // Invoke("BuildMesh", 2);

     //  BuildNav();

       



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
            Parent.BedQual = BedQual;
        }
        //Light Shifts level 0
        if (IsShiftingLightsB)
        {
            foreach (Light l in Lights)
            {
                l.intensity = Mathf.Lerp(l.intensity, 800, 0.01f);
            }
        }
        if (IsShiftingLightsN)
        {
            foreach (Light l in Lights)
            {
                
              
                l.intensity = Mathf.Lerp(l.intensity, 0, 0.01f);
                l.GetComponent<MeshRenderer>().material = OffLights;
                l.GetComponent<AudioSource>().Stop();
            }
        }

    }
}
