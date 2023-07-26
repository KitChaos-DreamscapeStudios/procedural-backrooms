using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CrawlGen :  Generation
{
    float elap;
    bool StartCheck;
    float elapForWood;
    public List<AudioSource> Creaks;
    public AudioSource HumNoise;
    float BurstRand;
    float ElapForWoodBurst;
    public GameObject WoodenBeast;
    public AudioSource WoodenBeastScreech;
    public bool BeastActive;
    public void Start()
    {
        BurstRand = Random.Range(10, 25);
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

        StartCoroutine(GenerateFromCenter());
        InvokeRepeating("CheckDespawn", 1, 1);

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
    IEnumerator GenerateFromCenter()
    {
        for (; ; )
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
                yield return null;
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

    public void Update()
    {
        elap += Time.deltaTime;
        elapForWood += Time.deltaTime;
     
        
        if(Playerstats.Sanity > 70)
        {
            ElapForWoodBurst += Time.deltaTime;
            if(ElapForWoodBurst > BurstRand + (Playerstats.Sanity / 3))
            {
                for (int i = 0; i < 8; i++)
                {
                    MoveWood();
                }
                ElapForWoodBurst = 0;
                BurstRand = Random.Range(10, 25);
            }
        }
        if (elap > 2)
        {
            StartCheck = true;
        }
        if (StartCheck)
        {
            Chunks = Chunks.Where(item => item != null).ToList();
            Center = PlayerIn.coords;
            Playerstats.SanityDrain = SanityDrain;
        }
        
            if (elapForWood > Playerstats.Sanity / 2)
            {
            MoveWood();

                elapForWood = 0;
            }
        

    }
    void MoveWood()
    {

        Creaks[Random.Range(0, Creaks.Count)].Play();
        var Pillar = GetRandomWithTag("Pillars");
        Vector3 NewPos = Playerstats.transform.position + (Random.onUnitSphere * Playerstats.Sanity);
        NewPos.y = -2.5537f;
        Pillar.transform.position = NewPos;
    }

    float DifferenceInCoords(Coords a, Coords b)
    {
        var xDist = Mathf.Abs(a.X - b.X);
        var zDist = Mathf.Abs(a.Z - b.Z);
        return (xDist + zDist);
    }
    GameObject GetRandomWithTag(string Tag)
    {
        var TaggedObjs = GameObject.FindGameObjectsWithTag(Tag);
        return TaggedObjs[Random.Range(0, TaggedObjs.Length)];
    }
}
