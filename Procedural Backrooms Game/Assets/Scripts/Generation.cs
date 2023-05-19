using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generation : MonoBehaviour
{
    public Chunk PlayerIn;
    public List<GameObject> ChunkForLevel;
    public Coords Center;
    public List<Chunk> Chunks;
    public delegate void Function(Coords c);
    public PlayerStats Playerstats;
    public float SanityDrain = 1;
    public GameObject LastChunk;
    public float BedQual;
    //This is the overrarching Generative script, with many chunk duchies filled with serfs like saving, entity handling, and more.
    
    public abstract void GenerateChunk(Coords c, bool IsCore);

    public void DoAtChunk(Function f, Coords c)
    {
        f(c);
    }
}
[System.Serializable]
public struct Coords
{
    public int X;
    public int Y;
    public int Z;
    //Most levels are 110, meaning for every 1 x, add 1 y, and 0 Z, making flat levels. However, some may be 1, 0.5, 0, or even 1,1,0.1
    //Chuck size is 80
    public Coords(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
}
