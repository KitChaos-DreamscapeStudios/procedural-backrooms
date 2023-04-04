using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Level0Generation : Generation
{
    public void Start()
    {
        LastChunk = ChunkForLevel[Random.Range(0, ChunkForLevel.Count)];
        Center = new Coords(0, 0, 0);
        for (int x = -2; x < 3; x++)
        {
            for (int z = -2; z < 3; z++)
            {

                GenerateChunk(new Coords(x, 0, z));


            }
        }
        Playerstats = GameObject.Find("Player").GetComponent<PlayerStats>();
        Playerstats.LevelStats = this;
       

        InvokeRepeating("CheckDespawn", 1, 1);
        InvokeRepeating(nameof(GenerateFromCenter), 1.1f, 1.1f);
    }
    public override void GenerateChunk(Coords c)
    {
        var ToSpawn = ChunkForLevel[Random.Range(0, ChunkForLevel.Count)];
        
        var Rand = Random.Range(0, maxInclusive: 1);
        if(Rand == 0)
        {
            var C = Instantiate(ToSpawn, new Vector3(c.X * 80, c.Y, c.Z * 80), Quaternion.identity);
            var ChunkData = C.GetComponent<Chunk>();
            ChunkData.Parent = this;
            Chunks.Add(ChunkData);
            // Debug.Log(ChunkData.Structs.Count);
            try
            {
                ChunkData.SpawnStuff(ChunkData.Structs[0]);
                ChunkData.coords = c;
            }
            catch (System.Exception)
            {

                //Debug.Log(ChunkData.Structs.Count);
            }
            LastChunk = C;
        }
        else
        {
            var C = Instantiate(LastChunk.gameObject, new Vector3(c.X * 80, c.Y, c.Z * 80), Quaternion.identity);
            var ChunkData = C.GetComponent<Chunk>();
            ChunkData.Parent = this;
            Chunks.Add(ChunkData);
            // Debug.Log(ChunkData.Structs.Count);
            try
            {
                ChunkData.SpawnStuff(ChunkData.Structs[0]);
                ChunkData.coords = c;
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
                Destroy(c.gameObject);
            }
        }
    }
    public void Update()
    {

        Chunks = Chunks.Where(item => item != null).ToList();
        Center = PlayerIn.coords;
        Playerstats.SanityDrain = SanityDrain;
    }
    
    float DifferenceInCoords(Coords a, Coords b)
    {
        var xDist = Mathf.Abs(a.X - b.X);
        var zDist = Mathf.Abs(a.Z - b.Z);
        return (xDist + zDist);
    }
    // Start is called before the first frame update

}
