using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public abstract class Damager : MonoBehaviour
{
    public float Damage;
    public float Health;
    public string DeathMessage;
    public bool HasHeardPlayer;
    public bool DontPreventSleep;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject)
        {
            if (col.gameObject.transform)
            {
                if (col.gameObject.transform.parent)
                {
                    if (col.gameObject.transform.parent.GetComponent<DamageSpear>())
                    {
                        Health -= col.gameObject.transform.parent.GetComponent<DamageSpear>().Damage;
                        OnTakeDamage();
                    }
                }
            }
        }
        
        //if (col.gameObject.transform.parent.GetComponent<DamageSpear>())
        {
          
        }
    }
   
    public abstract void OnTakeDamage();
    public abstract void OnDamage();
    public abstract void Die();
   
}
public static class GameObjectExtensions
{
    public static IEnumerator ShakeObject(this GameObject gameObject, float HowMuch, int HowMany)
    {
        ///Does Not Work
        for (int i =0;i<HowMany; i++ )
        {
            var off = Random.onUnitSphere * HowMuch;
            gameObject.transform.position += off*Random.Range(-3, 3);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.position -= off * Random.Range(-3, 3);
        }
       // return null;
        

    }
    public static int GetNearObjectsWithTag(this GameObject gameObject, string Tag, float Range, out GameObject Nearest)
    {
        //Debug.Log(gameObject.transform.position);
        ///Gets Nearby Objects With Tag, Range Customizable per call
        int total = 0;
        
        float CurClosest = Mathf.Infinity; 
        GameObject CurClosestObject = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tag))
        {
            if (obj.activeSelf)
            {
                if (Vector3.Distance(obj.gameObject.transform.position, gameObject.transform.position) < CurClosest)
                {
                    CurClosest = Vector3.Distance(obj.gameObject.transform.position, gameObject.transform.position);
                    CurClosestObject = obj;
                }
                if (Vector3.Distance(obj.gameObject.transform.position, gameObject.transform.position) < Range)
                {
                    total += 1;
                }
            }
           
        }
        Nearest = CurClosestObject;
        return total;
    }
    public static void MoveForward(this GameObject gameObject, float Slowness)
    {
        
        if (Time.deltaTime != 0)
        {
            gameObject.transform.position += gameObject.transform.forward / Slowness;
        }
    }
    public static GameObject GetClosestEnemy(this GameObject gameObject,GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = gameObject.transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
       
        return tMin.gameObject;
    }



}

