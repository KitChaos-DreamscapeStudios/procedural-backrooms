using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FirebrickGen : Generation
{
    //public float FlickerTimer;
    bool HasGeneratedMap;
    public bool IsEnflamen;
    public float FlameTimer;
    //public AudioSource Amb;
    public AudioSource HeatAmb;
    public SpriteRenderer HeatFX;
    public bool IsSafe;
    float AwaitNewCook;
    public List<ClosableDoor> SafeDoors;
    public float AshPoison;
    
    
    public void Start()
    {
        HeatAmb = GetComponent<AudioSource>();
        Invoke("Generate", 0.5f);
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

        var Rand = Mathf.RoundToInt(Random.Range(0, maxInclusive: 35));
        // Debug.Log(Rand);
        if (Rand == 0)
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
                    if (c.coords.X == x + Center.X && c.coords.Z == z + Center.Z)
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
            AshPoison += Time.deltaTime;
            if(AshPoison > 60)
            {
                if(Playerstats.MaxHealth > 20)
                {
                    Playerstats.MaxHealth -= 2;
                }
              

                AshPoison = 0;
                //Add a cough noise later
            }
            if(SafeDoors.Count > 0)
            {
                IsSafe = true;
            }
            else
            {
                IsSafe = false;
            }
            Chunks = Chunks.Where(item => item != null).ToList();
            Center = PlayerIn.coords;
            Playerstats.SanityDrain = SanityDrain;
            Playerstats.BedQuality = BedQual;
            if (IsEnflamen)
            {
                FlameTimer += Time.deltaTime;
            }
            
            HeatAmb.volume = (FlameTimer / 80);
            HeatFX.color = new Color(1, 0.2f, 0, (FlameTimer / 80) - 0.3f);
            HeatFX.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            HeatFX.transform.LookAt(Camera.main.transform);
            if (IsSafe)
            {
                HeatFX.gameObject.SetActive(false);
            }
            else
            {
                HeatFX.gameObject.SetActive(true);
            }
            if(FlameTimer > 120)
            {
                AwaitNewCook = 0;
                FlameTimer = 0;
                IsEnflamen = false;
            }
            if (!IsEnflamen)
            {
                AwaitNewCook += Time.deltaTime;
            }
            if(AwaitNewCook > 30)
            {
                IsEnflamen = true;
            }
            if(FlameTimer>80 && !IsSafe)
            {
                Playerstats.TakeDamage(4 * Time.deltaTime, "Cooked");
            }
            //level specific event: The light shift
           
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
