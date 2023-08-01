using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class WarehouseGen : Generation
{
    float elap;
    bool StartCheck;
    float ElapTillBoxKnock;
    public bool OutLights;
    public float OutLightTimer;
    public AudioSource BooWomp;
    public AudioSource BoxKnock;
    public float ThreatLevel;
    public AudioSource Siren;
    public float SirenSin;
    public List<AudioSource> Ambs;
    public void Start()
    {
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
        OutLightTimer += Time.deltaTime;

        SirenSin += Time.deltaTime;
        if (ThreatLevel > 0)
        {
            ThreatLevel -= Time.deltaTime / 4;
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
            if (OutLightTimer > Playerstats.Sanity)
            {
                if (!OutLights)
                {
                    BooWomp.Play();
                }
                OutLights = !OutLights;
                OutLightTimer = (-100 + Playerstats.Sanity);
                
              
              
                
            }
            if (OutLights)
            {
                ElapTillBoxKnock += Time.deltaTime;
                if (ElapTillBoxKnock > Playerstats.Sanity + Random.Range(-20, 20))
                {
                    ElapTillBoxKnock = 0;
                    BoxKnock.transform.position = Playerstats.transform.position + Random.insideUnitSphere * 20;
                    BoxKnock.Play();
                    ThreatLevel += 20 - Vector3.Distance(BoxKnock.transform.position, Playerstats.transform.position);
                }
                foreach (AudioSource item in Ambs)
                {
                    item.Stop();
                }
            }
            else
            {
                foreach (AudioSource item in Ambs)
                {
                    if (!item.isPlaying)
                    {
                        item.Play();
                    }
                   
                }
            }
            foreach (Chunk chunk in Chunks)
            {
                foreach (Light light in chunk.Lights)
                {
                    light.enabled = !OutLights;
                    //light.GetComponent<AudioSource>().enabled = !OutLights;
                    if (ThreatLevel > 60)
                    {
                        light.color = new Color(1, Mathf.Sin(SirenSin), Mathf.Sin(SirenSin));
                    }
                    
                        
                   
                }
            }
            if (ThreatLevel > 60)
            {
                OutLights = false;
                if (!Siren.isPlaying)
                {
                    Siren.Play();
                }
                   

            }
            else
            {
                Siren.Stop();
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
