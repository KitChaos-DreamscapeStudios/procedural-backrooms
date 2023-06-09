using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloorLiquid : MonoBehaviour
{
    public Terrain Water;
    //public AudioSource Splash;
    public List<Vector3> PushPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        Water = GetComponent<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {

        Debug.Log("Hit");
        Vector3 Pos = ConvertWordCor2TerrCor(col.transform.position);
        PushPoint.Add(Pos);
        float[,] MyArray;
        MyArray = Water.terrainData.GetHeights((int)Pos.x, (int)Pos.y, Water.terrainData.heightmapResolution, Water.terrainData.heightmapResolution);
        //for (int y = 0; y <100; y++)
        //{
        //    for (int x = 0; x < 100; x++)
        //    {
        //        MyArray[x, y] = 0f;
        //    }
        //}
        
        Water.terrainData.SetHeights((int)Pos.x, (int)Pos.y, MyArray);
        Water.Flush();
    }
   
    //Stolen this bit below
    private Vector3 ConvertWordCor2TerrCor(Vector3 wordCor)
    {
        Vector3 vecRet = new Vector3();
        Terrain ter = Water;
        Vector3 terPosition = ter.transform.position;
        vecRet.x = ((wordCor.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.alphamapWidth;
        vecRet.z = ((wordCor.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.alphamapHeight;
        return vecRet;
    }

}
