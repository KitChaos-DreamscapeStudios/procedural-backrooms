using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOneFromList : MonoBehaviour
{
    public List<GameObjectRarity> PossibleKeeps;
    GameObject ChosenObject;
    public bool RequireOne;
    // Start is called before the first frame update
    void Start()
    {
        ChosenObject = ChooseOneFromList();
        if(ChosenObject != null)
        {
            DestroyNonChosen();
        }
        
    }
    GameObject ChooseOneFromList()
    {
        Again:;
        var FirstKeep = PossibleKeeps[Random.Range(0, PossibleKeeps.Count)];
        var Check = Random.Range(0, maxInclusive: 100);
        if(Check <= FirstKeep.Rarity)
        {
            if (RequireOne)
            {
                goto Again;
            }
            else
            {
                return null;
            }
          
        }
        else
        {
            return FirstKeep.target;
        }
    }
    void DestroyNonChosen()
    {
        foreach (GameObjectRarity item in PossibleKeeps)
        {
            if(item.target != ChosenObject)
            {
                Destroy(item.target);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public struct GameObjectRarity
{
    public GameObject target;
    public float Rarity;
    public GameObjectRarity(GameObject g, float R)
    {
        this.target = g;
        this.Rarity = R;
    }
}
