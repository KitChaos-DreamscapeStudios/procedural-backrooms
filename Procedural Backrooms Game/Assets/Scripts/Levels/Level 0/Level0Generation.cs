using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Generation : Generation
{
    public override void GenerateChunk(Coords c)
    {
        var C = Instantiate(Chunk, new Vector3(c.X, c.Y, c.Z), Quaternion.identity);
        var ChunkData = C.GetComponent<Chunk>();
        ChunkData.SpawnStuff(ChunkData.Structs[Random.Range(0, ChunkData.Structs.Count)]);
    }
    //Check if the player is far enough away to spawn new chunks
    public void Update()
    {
        
    }
    // Start is called before the first frame update

}
