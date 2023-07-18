using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Level0Generation : Generation
{

    public float FlickerTimer;
    bool HasGeneratedMap;
    public GameObject FakeWalls;
    float FakeWallTimer;
    public void Start()
    {
        Invoke("Generate", 2);  
    }
    public void Generate()
    {
        Rand = Random.Range(30, 31);
        WallRand = 20;

        LastChunk = ChunkForLevel[Random.Range(0, ChunkForLevel.Count)];
        Center = new Coords(0, 0, 0);
        for (int x = -2; x < 3; x++)
        {
            for (int z = -2; z < 3; z++)
            {

                GenerateChunk(new Coords(x, 0, z), true);


            }
        }
        Playerstats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Playerstats.LevelStats = this;

        HasGeneratedMap = true;
        InvokeRepeating("CheckDespawn", 1, 1);
        InvokeRepeating(nameof(GenerateFromCenter), 1.1f, 1.1f);
    }
    public override void GenerateChunk(Coords c, bool IsCore = false)
    {
        var ToSpawn = ChunkForLevel[Random.Range(0, ChunkForLevel.Count)];
        
        var Rand = Mathf.RoundToInt( Random.Range(0, maxInclusive: 35));
       // Debug.Log(Rand);
        if(Rand == 0)
        {
            var C = Instantiate(ToSpawn, new Vector3(c.X * 80, c.Y, c.Z * 80), Quaternion.identity);
            var ChunkData = C.GetComponent<Chunk>();
            ChunkData.Parent = this;
            if (IsCore)
            {
                PlayerIn = ChunkData;
            }
            Chunks.Add(ChunkData);
            //Debug.Log("Spawnednew");
            
            // Debug.Log(ChunkData.Structs.Count);
            try
            {
                ChunkData.coords = c;
                if (DifferenceInCoords(PlayerIn.coords, ChunkData.coords) > 3)
                {
                    Destroy(ChunkData.gameObject);
                }
                else
                {
                    ChunkData.SpawnStuff(ChunkData.Structs[Random.Range(0, ChunkData.Structs.Count)]);
                }
                
               
            }
            catch (System.Exception)
            {

                //Debug.Log(ChunkData.Structs.Count);
            }
            LastChunk = ToSpawn;
        }
        else
        {
          //  Debug.Log("SpawnedSame");
            var C = Instantiate(LastChunk.gameObject, new Vector3(c.X * 80, c.Y, c.Z * 80), Quaternion.identity);
            var ChunkData = C.GetComponent<Chunk>();
            ChunkData.Parent = this;
            if (IsCore)
            {
                PlayerIn = ChunkData;
            }
            Chunks.Add(ChunkData);
            // Debug.Log(ChunkData.Structs.Count);
            try
            {
                ChunkData.coords = c;
                if (DifferenceInCoords(PlayerIn.coords, ChunkData.coords) > 3)
                {
                    Destroy(ChunkData.gameObject);
                }
                else
                {
                    ChunkData.SpawnStuff(ChunkData.Structs[Random.Range(0, ChunkData.Structs.Count)]);

                }
            }
            catch (System.Exception)
            {

                //Debug.Log(ChunkData.Structs.Count);
            }
        }
       
        //Debug.Log(C.transform.position);
        
        
        
        
    }
    void GenerateFromCenter()
    {
        for (int x = -2; x < 3; x++)
        {
            for (int z = -2; z < 3; z++)
            {
                bool Occupied = false;
                foreach (Chunk c in Chunks)
                {
                    if(c.coords.X == x+Center.X && c.coords.Z == z + Center.Z)
                    {
                        Occupied = true;
                    }
                }
                if (!Occupied)
                {
                    GenerateChunk(new Coords(x + Center.X, 0, z + Center.Z));
                }
               


            }
        }
       
    }
    //Check if the player is far enough away to spawn new chunks
    void CheckDespawn()
    {
        foreach (Chunk c in Chunks)
        {
            if (DifferenceInCoords(PlayerIn.coords, c.coords) > 3)
            {
                if (c.gameObject != null)
                {
                    Destroy(c.gameObject);
                }
              
            }
        }
    }
    float Rand;
    float WallRand;
    public void Update()
    {
        if (HasGeneratedMap)
        {
            Chunks = Chunks.Where(item => item != null).ToList();
            Center = PlayerIn.coords;
            Playerstats.SanityDrain = SanityDrain;
            Playerstats.BedQuality = BedQual;

            //level specific event: The light shift
            if (Playerstats.Sanity < 50)
            {
                FlickerTimer += Time.deltaTime;
                if (FlickerTimer >= Rand)
                {
                    FlickerTimer = 0;
                    Rand = Random.Range(20, 40);
                    var BrightorNight = Random.Range(0, 2);
                    if (BrightorNight == 0)
                    {
                        //bright
                        PlayerIn.IsShiftingLightsB = true;
                    }
                    else
                    {
                        //Night
                        PlayerIn.IsShiftingLightsN = true;
                    }
                }

            }
            //level Specific effect: The Fake Walls
            if(Playerstats.Sanity < 40)
            {
                FakeWallTimer += Time.deltaTime;
                if (FakeWallTimer >= WallRand)
                {
                    FakeWallTimer = 0;
                    WallRand = Random.Range(30, 60);
                    Instantiate(FakeWalls, Playerstats.transform.position - new Vector3(0, 10, 0), Quaternion.identity);
                }
            }
            
        }
        
       
    }
    
    float DifferenceInCoords(Coords a, Coords b)
    {
        var xDist = Mathf.Abs(a.X - b.X);
        var zDist = Mathf.Abs(a.Z - b.Z);
        return (xDist + zDist);
    }
    // Start is called before the first frame update

}
