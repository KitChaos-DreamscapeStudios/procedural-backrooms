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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<DamageSpear>())
        {
            Health -= col.gameObject.GetComponent<DamageSpear>().Damage;
            OnTakeDamage();
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
        for (int i =0;i<HowMany; i++ )
        {
            var off = Random.onUnitSphere * HowMuch;
            gameObject.transform.position += off*Random.Range(-3, 3);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.position -= off * Random.Range(-3, 3);
        }
       // return null;
        

    }
    
}

