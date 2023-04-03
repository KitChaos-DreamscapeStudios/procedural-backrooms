using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Generation : Generation
{
    public override void GenerateChunk(Coords c)
    {
        var C = Instantiate(Chunk, new Vector3(c.X, c.Y, c.Z), Quaternion.identity);
        var ChunkData = C.GetComponent<Chunk>();
        ChunkData.Parent = this;
        Debug.Log(ChunkData.Structs.Count);
        try
        {
            ChunkData.SpawnStuff(ChunkData.Structs[0]);
        }
        catch (System.Exception)
        {

            //Debug.Log(ChunkData.Structs.Count);
        }
          
        
        
    }
    //Check if the player is far enough away to spawn new chunks
    public void Update()
    {
        
    }
    // Start is called before the first frame update

}
